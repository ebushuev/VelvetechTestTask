using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TodoApiDTO.Models;

namespace TodoApiDTO.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            if (ex is KeyNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(JsonSerializer.Serialize(
                    new ErrorDetails { StatusCode = 404, Message = ex.Message }
                    ));
                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails { StatusCode = 500, Message = $"Something went wrong: {ex.Message}" }));
        }
    }
}
