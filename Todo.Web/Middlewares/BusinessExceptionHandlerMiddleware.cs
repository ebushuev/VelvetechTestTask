using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Todo.Core.Exceptions;
using Todo.Web.Models;

namespace Todo.Web.Middlewares
{
    public class BusinessExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private const string BadRequestExceptionMessage = "Bad request exception";
        private const string NotFoundExceptionMessage = "Not found exception";
        private const string InternalErrorExceptionMessage = "An internal exception has occurred";

        public BusinessExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<BusinessExceptionHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                await HandleBusinessExceptionAsync(context, logger, ex);
            }
        }


        protected virtual async Task HandleBusinessExceptionAsync(HttpContext context, ILogger log, BusinessException exception)
        {
            switch (exception)
            {
                case BadRequestException badRequestException:
                    {
                        log.LogError(badRequestException, BadRequestExceptionMessage);
                        await WriteResponseAsync(context, badRequestException.Message, (int)HttpStatusCode.BadRequest, badRequestException.Errors);
                        break;
                    }
                case NotFoundException notFoundException:
                    {
                        log.LogError(notFoundException, NotFoundExceptionMessage);
                        await WriteResponseAsync(context, notFoundException.Message, (int)HttpStatusCode.NotFound);
                        break;
                    }
                default:
                    {
                        log.LogError(exception, InternalErrorExceptionMessage);
                        await WriteResponseAsync(context, exception.Message, (int)HttpStatusCode.InternalServerError);
                        break;
                    }
            }
        }

        protected async Task WriteResponseAsync(HttpContext context, string errorMessage, int statusCode, IDictionary<string, IEnumerable<string>> errors = null)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(new ErrorResponse(errorMessage, errors).ToString());
        }
    }
}
