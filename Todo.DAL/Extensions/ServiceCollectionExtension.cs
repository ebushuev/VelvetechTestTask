using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.DAL.EF;
using TodoApi.DAL.Repositories;

namespace Todo.DAL.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
            .AddDbContext<TodoContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Db"));
            });

            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }
}
