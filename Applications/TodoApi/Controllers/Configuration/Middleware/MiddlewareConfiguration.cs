using Microsoft.AspNetCore.Builder;
using TodoApi.Middleware;

namespace TodoApi.Controllers.Configuration.Middleware
{
    public static class MiddlewareConfiguration
    {
        public static void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}