using Microsoft.AspNetCore.Builder;
using TodoApiDTO.Exceptions;

namespace TodoApiDTO.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalExceptionHandler>();
    }
}