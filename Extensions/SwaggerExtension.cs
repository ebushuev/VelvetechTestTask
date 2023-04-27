using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TodoApiDTO.Extensions;

public static class SwaggerExtension
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Test Swagger", Description = "Test Swagger bcuz Postman not FREE", Version = "v1"
                });
        });
    }

    public static void AddCustomSwaggerUI(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(option => { option.SwaggerEndpoint("/swagger/v1/swagger.json", "swagger demo api"); });
    }
}