using Microsoft.Extensions.DependencyInjection;
using TodoApplication.Services;
using TodoCore.Services;

namespace TodoApplication
{
    public static class ApplicationCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IAddTodoItemService, AddTodoItemService>();
            services.AddTransient<IGetTodoItemService, GetTodoItemService>();
            services.AddTransient<IGetTodoItemsService, GetTodoItemsService>();
            services.AddTransient<IDeleteTodoItemService, DeleteTodoItemService>();
            services.AddTransient<IUpdateTodoItemService, UpdateTodoItemService>();
            return services;
        }
    }
}
