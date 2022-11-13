using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDbContextData(this IServiceCollection services) =>
           services.AddScoped<IDbContext>(provider =>
                   provider.GetService<ApplicationDbContext>());
        public static IServiceCollection AddMSSqlDatabaseData(this IServiceCollection services,
           IConfiguration configuration) =>
             services.AddDbContext<ApplicationDbContext>(opts =>
                  opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
                  b.MigrationsAssembly("Data")));
    }
}
