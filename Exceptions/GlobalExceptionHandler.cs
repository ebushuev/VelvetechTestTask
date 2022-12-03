namespace TodoApiDTO.Exceptions
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using TodoApiDTO.Logger;

    /// <summary>
    /// Глобальный обработчик ошибок.
    /// </summary>
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string stackTrace;
            string message;

            var isCustomError = false;

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(ValidationException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = exception.StackTrace;
                isCustomError = true;
            }
            else if (exceptionType == typeof(DbUpdateConcurrencyException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }

            LoggerHelper.LogError(isCustomError, exception);

            var exceptionResult = JsonConvert.SerializeObject(
                new {
                    error = message,
                    stackTrace
                });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}