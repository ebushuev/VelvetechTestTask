using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Infrastructure.Data.Contexts;
using TodoApiDTO.Application.TodoItemBoundary.Presentation;

namespace TodoApiDTO.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(ITodoItemPresentation), typeof(TodoItemPresentation));
            services.AddScoped(x => new TodoItemPresentation(context: x.GetRequiredService<TodoContext>()));

            return services;
        }
    }
}
