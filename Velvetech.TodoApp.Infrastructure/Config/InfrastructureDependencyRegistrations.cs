using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Velvetech.TodoApp.Infrastructure.Data;
using Velvetech.TodoApp.Infrastructure.Repositories.Abstractions.Custom;
using Velvetech.TodoApp.Infrastructure.Repositories.Implementations.Custom;

namespace Velvetech.TodoApp.Infrastructure.Config
{
    public static class InfrastructureDependencyRegistrations
    {
        public static void AddInfrastructure(this IServiceCollection services) // Todo Add Sql Server from appsettings
        {

            services.AddDbContext<TodoContext>(opt =>
                opt.UseInMemoryDatabase("TodoList"));

            services.AddTransient<ITodoItemRepository, TodoItemRepository>();
        }
    }
}
