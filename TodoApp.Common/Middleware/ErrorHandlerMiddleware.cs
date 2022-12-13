using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using TodoApp.Common.Contract;

namespace TodoApp.Common.Middleware
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
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.OK;

                ApiResponse apiResponse = new ApiResponse();

                if (error is GlobalException)
                {
                    GlobalException exception = (GlobalException)error;
                    apiResponse.Code = exception.Code;
                    apiResponse.Message = exception.Message;
                }
                else
                {
                    apiResponse.Code = ResponseCode.InternalServerError;
                    apiResponse.Message = error.Message;
                }
                var excludeProperties = new[] { "applicationName" };
                var result = JsonConvert.SerializeObject(apiResponse, Formatting.Indented);
                await response.WriteAsync(result);
            }
        }
    }
}
