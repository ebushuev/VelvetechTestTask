using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System;
using TodoApi.BLL.Infrastructure;
using NLog;

namespace TodoApi.Logger
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
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
            HttpStatusCode status = HttpStatusCode.NotFound;
            string stackTrace = exception.StackTrace;
            string message = exception.Message;

            var isCustomError = false;

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(ValidationException))
            {
                LogManager.GetLogger("CustomError").Error(exception + Environment.NewLine);
            }
            else if (exceptionType != typeof(DbUpdateConcurrencyException))
            {
                status = exceptionType != typeof(DbUpdateConcurrencyException)? HttpStatusCode.InternalServerError: status;
                LogManager.GetLogger("SystemError").Error(exception + Environment.NewLine);
            }

            var exceptionResult = JsonConvert.SerializeObject(
                new
                {
                    error = message,
                    stackTrace
                });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
