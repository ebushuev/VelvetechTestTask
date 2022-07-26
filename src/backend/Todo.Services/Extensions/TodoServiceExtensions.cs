using Microsoft.Extensions.DependencyInjection;

namespace Todo.Services.Extensions
{
    public static class TodoServiceExtensions
    {
        public static void AddTodoService(this IServiceCollection services)
        {
            services.AddTransient<ITodoItemService, TodoItemService>();
        }
    }
}
