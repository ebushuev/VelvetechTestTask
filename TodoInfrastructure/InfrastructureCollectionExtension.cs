using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoCore.Data.Interfaces;
using TodoInfrastructure.DataAccess;
using TodoInfrastructure.DataAccess.Repositories;
using TodoInfrastructure.DataAccess.UOW;

namespace TodoInfrastructure
{
    public static class InfrastructureCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITodoItemRepository, TodoItemRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationDbContext)));
            });
            return services;

        }
    }
}
