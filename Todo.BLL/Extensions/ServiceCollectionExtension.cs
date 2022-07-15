using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Todo.BLL.Mappings;
using Todo.BLL.Services;
using Todo.DAL.Extensions;
using FluentValidation;
using Todo.BLL.Models;
using Todo.BLL.Validation;

namespace Todo.BLL.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterRepositories(configuration);
            services.AddAutoMapper(typeof(TodoProfile));
            services.AddValidatorsFromAssemblyContaining<TodoItemValidator>();
            services.AddScoped<ITodoService, TodoService>();
            return services;
        }
    }
}
