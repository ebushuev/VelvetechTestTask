using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TodoApi.Middlewares
{
    public class ExceptionLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger)
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
                _logger.LogError(ex,String.Empty,null);
                await HandleException(context, ex.GetBaseException());
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {

            HttpStatusCode status;
            if (ex is ArgumentException)
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (ex is NullReferenceException ||
               ex is InvalidOperationException)
            {
                status = HttpStatusCode.NotFound;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            var result = JsonSerializer.Serialize(new { title = status.ToString(), status, traceId = context.TraceIdentifier, message = ex.Message });

            return context.Response.WriteAsync(result);
        }
    }


}
