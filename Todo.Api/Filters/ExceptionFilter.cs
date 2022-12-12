using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Todo.Domain.Exceptions;

namespace TodoApiDTO.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var exception = context.Exception;
                HttpStatusCode statusCode;
                if (exception is ItemNotFoundException)
                {
                    statusCode = HttpStatusCode.NotFound;

                } else if (exception is ArgumentException)
                {
                    statusCode = HttpStatusCode.BadRequest;

                } else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                }
                context.Result = new ObjectResult(exception.Message) { StatusCode = (int)statusCode };
            }
        }
    }
}
