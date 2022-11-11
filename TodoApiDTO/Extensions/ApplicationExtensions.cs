using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApiDTO.DAL.Contexts;
using TodoApiDTO.Helpers;

namespace TodoApiDTO.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(configs =>
            {
                configs.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
            });
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>())
                {
                    dbContext.Database.Migrate();
                }
            }

            return app;
        }

        public static void UseApiResponseMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
