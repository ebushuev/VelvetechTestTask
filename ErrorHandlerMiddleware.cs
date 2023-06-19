using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System;

namespace TodoApiDTO
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                if (error is EntityNotFoundException)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                //await _next(context);
            }
        }
    }
}
