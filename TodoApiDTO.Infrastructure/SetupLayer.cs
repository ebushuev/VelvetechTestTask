using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TodoApiDTO.Domain;
using TodoApiDTO.Infrastructure.EfCore;

namespace TodoApiDTO.Infrastructure
{
    public static class SetupLayer
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoApiDTOContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITodoItemRepository, TodoItemRepository>();

            return services;
        }
    }
}
