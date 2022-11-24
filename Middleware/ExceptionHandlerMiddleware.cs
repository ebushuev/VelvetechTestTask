using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TodoApiDTO.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private const string ContentType = "application/json; charset=utf-8";

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                var responseBody = "Server error: something went wrong";

                var jsonBody = JsonSerializer.Serialize(responseBody);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = ContentType;
                await context.Response.WriteAsync(jsonBody, Encoding.UTF8);
            }
        }
    }
}
