using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApiDTO.Data.Repositories;

namespace TodoApiDTO.Data.Infrastructure
{
    public static class Registrations
    {
        
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TodoContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
        
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITodoItemRepository, TodoItemRepository>();

            return services;
        }
    }
}