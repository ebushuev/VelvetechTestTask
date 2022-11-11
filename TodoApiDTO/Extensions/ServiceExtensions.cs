using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using TodoApiDTO.BLL.Mappings;
using TodoApiDTO.BLL.Services.Abstractions;
using TodoApiDTO.BLL.Services.Implementations;
using TodoApiDTO.DAL.Contexts;
using TodoApiDTO.DAL.UnitOfWork;
using TodoApiDTO.Mappings;

namespace TodoApiDTO.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(configs =>
            {
                configs.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Todo API", 
                    Version = "v1",
                    Description = "An API to perform to do list operations",
                    Contact = new OpenApiContact
                    {
                        Name = "Bagrat Antonyan",
                        Email = "bag.antonyan@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/bagrat-antonyan/")
                    }
                });
            });
        }

        public static void AddCorsConfigurations(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddServiceConfigurations(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITodoItemService, TodoItemService>();

            services.AddAutoMapper(config =>
            {
                config.AddProfile<ApiMappingProfile>();
                config.AddProfile<BLLMappingProfile>();
            });
        }
    }
}
