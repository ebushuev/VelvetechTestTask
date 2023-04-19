using Microsoft.AspNetCore.Builder;
using TodoApiDTO.Middleware;

namespace TodoApiDTO.Controllers.Configuration.Middleware
{
    public static class MiddlewareConfiguration
    {
        public static void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}