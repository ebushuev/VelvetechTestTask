using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Velvetech.TodoApp.Infrastructure.Data;
using Velvetech.TodoApp.Infrastructure.Repositories.Abstractions.Custom;
using Velvetech.TodoApp.Infrastructure.Repositories.Implementations.Custom;

namespace Velvetech.TodoApp.Infrastructure.Config
{
    public static class InfrastructureDependencyRegistrations
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) // Todo Add Sql Server from appsettings
        {
            var connectionString = configuration.GetConnectionString("TodoItemsConn");
            services.AddDbContext<TodoContext>(opt => opt
                .UseSqlServer(connectionString)
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll), ServiceLifetime.Scoped);

            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        }

        public static IApplicationBuilder ApplyDbMigrations(this IApplicationBuilder app)
        {
            IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            TodoContext dbContext = serviceScope.ServiceProvider.GetRequiredService<TodoContext>();
            dbContext.Database.Migrate();
            return app;
        }
    }
}
