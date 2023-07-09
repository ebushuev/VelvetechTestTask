using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Todo.Domain.DTOs;
using Todo.Domain.Exceptions;

namespace Todo.Api.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                }));
            }
        }
    }
}