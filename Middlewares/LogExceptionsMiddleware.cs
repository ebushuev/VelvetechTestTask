using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;


namespace TodoApiDTO.Middlewares
{
    public class LogExceptionsMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogExceptionsMiddleware(RequestDelegate next, ILogger<LogExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                await HandleExceptionAsync(context, exp.GetBaseException());
            }
        }


        private static Task HandleExceptionAsync(HttpContext context, Exception exp)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new { Code = code, exp.Message, exp.StackTrace });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }


    public static class LogExceptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogException(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogExceptionsMiddleware>();
        }
    }
}