using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class DALRegistrator
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration, string sectionName) 
        {
            var connectionString = configuration.GetConnectionString(sectionName);

            services
                .AddDbContext<TodoContext>(options => options.UseSqlServer(connectionString))
                .AddTransient<IToDoRepository, ToDoRepository>();

            return services;
        }
    }
}
