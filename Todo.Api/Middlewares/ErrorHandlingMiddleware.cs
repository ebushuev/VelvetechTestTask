using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Velvetech.Todo.Api.Middlewares
{
  public class ErrorHandlingMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
        _logger.LogError(ex, "Error while executing request");
        await HandleExceptionAsync(context, ex);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      int statusCode;
      var message = "Internal Server Error.";

      switch (exception)
      {
        case ArgumentNullException _:
        case ArgumentException _:
          statusCode = (int)HttpStatusCode.BadRequest;
          message = exception.Message;
          break;
        default:
          statusCode = (int)HttpStatusCode.InternalServerError;
          break;
      }

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = statusCode;

      return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = message }));
    }
  }
}
