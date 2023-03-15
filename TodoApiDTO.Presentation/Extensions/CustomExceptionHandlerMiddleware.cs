using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using TodoApiDTO.Application;
using TodoApiDTO.Domain;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace TodoApiDTO.Presentation.Extensions
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is NotFoundException)
            {
                _logger.LogWarning("Not Found");

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exception is ValidationException)
            {
                _logger.LogWarning("Bad Request");

                var result = JsonConvert.SerializeObject(new { message = exception.Message });

                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

                return context.Response.WriteAsync(result);
            }
            else
            {
                _logger.LogError(exception, "Very bad!");

                var result = JsonConvert.SerializeObject(new { message = "Something is wrong." });

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return context.Response.WriteAsync(result);
            }

            return Task.CompletedTask;
        }
    }

    public static class SetupCustomExceptionHandler
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
