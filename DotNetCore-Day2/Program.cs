
using DotNetCore_Day2.Data;
using DotNetCore_Day2.Repository;
using DotNetCore_Day2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace DotNetCore_Day2
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
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddSingleton<IGuidSingletonService, GuidSingletonService>();
            builder.Services.AddScoped<IGuidScopedService, GuidScopedService>();
            builder.Services.AddTransient<IGuidTransientService, GuidTransientService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateIssuerSigningKey = true,
                     ValidateLifetime = true,
                     ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                     ValidAudience = builder.Configuration["AppSettings:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]))
                 }
                 );

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("admin")
                );

                options.AddPolicy("Admin&Vender", policy =>
                    policy.RequireRole("admin","vender")
                );
                options.AddPolicy("AllowAll", policy =>
                    policy.RequireRole("admin","vender","customer")
                );
            });

            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("My API");
                    options.WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
