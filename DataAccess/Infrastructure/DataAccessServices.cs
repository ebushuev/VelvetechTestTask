using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DataAccessServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer("Data Source=(localdb)\\local;Initial Catalog=TodosDB;Integrated Security=True"));
            services.AddTransient<ITodoRepository, TodoRepository>();
            return services;
        }
    }
}
