
using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.MiddleWares;
using Event_Management_System.Profiles;
using Event_Management_System.Repository;
using Event_Management_System.Repository.Interfaces;
using Event_Management_System.Services;
using Event_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;

namespace Event_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen(options =>
            {

                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("bearer", document)] = []
                });
            });

            builder.Services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer= builder.Configuration["Appsettings:Issuer"],
                        ValidAudience = builder.Configuration["Appsettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Appsettings:Token"])),

                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("AdminOrOrganizer", policy =>
                    policy.RequireRole("Admin", "Organizer")
                );

                options.AddPolicy("ForAll", policy =>
                    policy.RequireRole("Admin", "Organizer","Attendee")
                );
            });

            builder.Services.AddRateLimiter(options =>
            {
                options.AddPolicy("PerUserPolicy", httpcontext =>
                {
                    var UserId = httpcontext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (UserId == null)
                    {
                        throw new UnauthorizedAccessException("User is not authenticated");
                    }

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: UserId,
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            Window = TimeSpan.FromMinutes(1),
                           
                        });
                });

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.ContentType = "application/json";

                    await context.HttpContext.Response.WriteAsync(
                        "{\"message\": \"Rate limit exceeded. Try again after 1 minute.\"}", token
                        );

                };
            });



            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();

            

            builder.Services.AddAutoMapper(typeof(Program));
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("My API");
                    options.WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
                });
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRateLimiter();

            app.MapControllers();

            app.Run();
        }
    }
}
