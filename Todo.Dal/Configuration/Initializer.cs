using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Common.RepositoryInterfaces;
using Todo.Dal.Context;
using Todo.Dal.Repositories;

namespace Todo.Dal.Configuration
{
    public static class Initializer
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TodoDb");
            services.AddDbContext<TodoContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddSingleton(typeof(ITodoContextFactory), new TodoContextFactory(connectionString));
            services.AddTransient<ITodoRepository, TodoRepository>();
            
            return services;
        }
    }
}
