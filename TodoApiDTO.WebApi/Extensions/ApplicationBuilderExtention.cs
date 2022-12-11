using Microsoft.AspNetCore.Builder;

namespace TodoApiDTO.WebApi.Extensions
{
    public static class ApplicationBuilderExtention
    {
        public static IApplicationBuilder UseServices(this IApplicationBuilder app) =>
            app.AddUseSwagger();


        private static IApplicationBuilder AddUseSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
