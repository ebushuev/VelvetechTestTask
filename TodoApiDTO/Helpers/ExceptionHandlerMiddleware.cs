using Microsoft.AspNetCore.Http;
using System.Net;
using System;
using System.Threading.Tasks;
using TodoApiDTO.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TodoApiDTO.BLL.Services.Abstractions;
using TodoApiDTO.Shared.Exceptions;

namespace TodoApiDTO.Helpers
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _loggerService.LogError($"Something went wrong: {exception}");

                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = ApiResponse<string>.Fail(exception.Message);

                switch (exception)
                {
                    case BadRequestException badRequest:
                        {
                            responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        }
                    case TodoItemNotFoundException notFound1:
                    case DbUpdateConcurrencyException notFound2:
                        {
                            responseModel.StatusCode = (int)HttpStatusCode.NotFound;
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                            break;
                        }
                    case BaseException baseEx:
                        {
                            responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                            break;
                        }
                    case Exception ex:
                        {
                            responseModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                        }
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
