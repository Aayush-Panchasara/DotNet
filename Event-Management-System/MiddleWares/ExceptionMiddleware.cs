using Azure;
using Event_Management_System.MiddleWares.CustomeException;
using Microsoft.AspNetCore.Components.Routing;
using System.Runtime.CompilerServices;

namespace Event_Management_System.MiddleWares
{
    public class ExceptionMiddleware
    {
        public readonly RequestDelegate _next;
        public readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context) {
            try
            {
                //Console.WriteLine("Before");
                await _next(context);
                //Console.WriteLine("After");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

                var result = new
                {
                    message = ex.Message,
                    statusCode = response.StatusCode
                };

                await response.WriteAsJsonAsync(result);


            }





        }
    }
}
