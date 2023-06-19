using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System;
using TodoApiDTO.BLL.Exceptions;

namespace TodoApiDTO
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

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
                _logger.LogError(error, string.Empty);
                var response = context.Response;
                if (error is EntityNotFoundException)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
        }
    }
}
