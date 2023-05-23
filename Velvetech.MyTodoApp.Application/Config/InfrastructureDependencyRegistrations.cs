using Microsoft.Extensions.DependencyInjection;
using Velvetech.MyTodoApp.Application.Services.Abstractions;
using Velvetech.MyTodoApp.Application.Services.Implementations;

namespace Velvetech.TodoApp.Infrastructure.Config
{
    public static class InfrastructureDependencyRegistrations
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ITodoItemService, TodoItemService>();
            services.AddTransient<ITodoItemService, TodoItemService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
