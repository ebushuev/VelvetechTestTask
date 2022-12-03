namespace TodoApiDTO.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using TodoApiDTO.Exceptions;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalExceptionHandler>();
    }
}