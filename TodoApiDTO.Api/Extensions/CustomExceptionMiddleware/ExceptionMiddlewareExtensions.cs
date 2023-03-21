using Microsoft.AspNetCore.Builder;

namespace TodoApiDTO.Api.Extensions.CustomExceptionMiddleware
{
    public static class ExceptionMiddlewareExtensions
    {
        #region Static

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        #endregion
    }
}