using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.DAL;

namespace Todo.Api.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("TodoDB")));
        }
    }
}
