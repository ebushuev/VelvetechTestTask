using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.DAL.Interfaces;
using ToDo.DAL.Repositories;

namespace ToDo.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITodoDbContext, TodoDbContext>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddDbContext<TodoDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("db")));
            
            return services;
        }
    }
}