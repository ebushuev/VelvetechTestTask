using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Todo.Application.Exceptions;

namespace Todo.WebApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
            catch (Exception error)
            {
                _logger.LogError(error?.Message);

                switch (error)
                {
                    case NotFoundException e:
                        var actionResult = new NotFoundResult();
                        await actionResult.ExecuteResultAsync(new ActionContext
                        {
                            HttpContext = context
                        });
                        break;
                    default:
                        var badRequestResult = new BadRequestResult();
                        await badRequestResult.ExecuteResultAsync(new ActionContext
                        {
                            HttpContext = context
                        });
                        break;
                }
            }
        }
    }
}
