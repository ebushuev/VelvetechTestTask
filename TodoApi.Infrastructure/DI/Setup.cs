using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Domain.Interfaces;
using TodoApi.Infrastructure.Repositories;
using TodoApi.Models;

namespace TodoApi.Infrastructure.DI
{
    public static class Setup
    {
        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext

            services.AddDbContext<TodoContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(TodoContext)));
            });

            #endregion

            #region Repositories 

            services.AddScoped<ITodoItemRepository, TodoItemRepository>();

            #endregion
        }
    }
}
