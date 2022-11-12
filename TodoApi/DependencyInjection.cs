using Application.Common.Mapping;
using Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TodoApi.Middleware;

namespace TodoApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCorsWepApi(this IServiceCollection services) =>
           services.AddCors(options =>
           {
               options.AddPolicy("AllowAll", policy =>
                   policy
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
           });

        public static IServiceCollection AddAutoMapperWepApi(this IServiceCollection services) =>
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IDbContext).Assembly));
            });
        public static IServiceCollection AddSwaggerGetWepApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Todo Api by Pavel",
                    Description = "An application for work with Todo!"
                });
            });
            return services;
        }

        public static IApplicationBuilder AddErrorHandingMiddlewareWebApi(this IApplicationBuilder app) 
            => app.UseMiddleware<ErrorHandlingMiddleware>();
        public static IApplicationBuilder AddSwaggerUIWebApi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
