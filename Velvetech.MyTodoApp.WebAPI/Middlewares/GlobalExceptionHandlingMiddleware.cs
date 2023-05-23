﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Velvetech.MyTodoApp.WebAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception)
            {
                //_logger.LogInformation($"Exception happened whith the message \"{ex.Message}\" in the endpoint \"{context.Request.Path}\"");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = $"An internal server error has occured"
                };

                var json = JsonSerializer.Serialize(problem);
                
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
