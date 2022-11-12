using Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TodoApiDTO.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
            => (_next, _logger) = (next, logger);

        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            object errors = null;
            var code = HttpStatusCode.InternalServerError;
  
            switch (ex)
            {
                case ValidationException validationException:
                   _logger.LogError(ex, "Validation exeption");
                    code = HttpStatusCode.BadRequest;
                    errors = validationException.Errors;
                    break;

                case NotFoundException notFoundEx:
                    _logger.LogError(ex, "Item not found");
                    errors = notFoundEx.Errors;
                    code = notFoundEx.Code;
                    break;

                case DbUpdateConcurrencyException dbUpdate:
                    _logger.LogError(ex, "Db Update Concurrency Exception");
                    errors = string.IsNullOrWhiteSpace(dbUpdate.Message) ? "error" : dbUpdate.Message;
                    code = HttpStatusCode.NotFound;
                    break;

                case Exception e:
                    _logger.LogError(ex, "Internal Server error");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "error" : e.Message;
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;


            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(errors);

                await context.Response.WriteAsync(result);
            }
        }
    }
}
