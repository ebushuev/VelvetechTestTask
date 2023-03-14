using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Data;
using TodoApiDTO.Models;

namespace TodoApiDTO.Data
{
    public static class DataHelpers
    {
        public static void SetDbConnection(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(config.GetConnectionString("MsSqlAuth")));
        }

        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
        }
    }
}
