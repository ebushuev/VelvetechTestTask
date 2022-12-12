﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using System;
using System.Security.Policy;
using TodoApiDTO.Application.Todo;
using TodoApiDTO.Infrastructure.Database;
using TodoApiDTO.Infrastructure.Services.Todo;
using Microsoft.OpenApi.Models;

namespace TodoApiDTO.WebApi.Extensions
{
    public static class ConfigureServiceExtention
    {
        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddServices()
                .AddDatabase(configuration)
                .AddSwagg();

        private static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddScoped<ITodoItemsService, TodoItemsService>()
                .AddScoped<ITodoItemsRepository, TodoItemsContainer>();

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<TodoContext>(opt => opt
                .UseSqlServer(configuration.GetConnectionString("Todo")));

        private static IServiceCollection AddSwagg(this IServiceCollection services) =>
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "ToDo API"
                    });

                    c.EnableAnnotations();
                });
    }
}
