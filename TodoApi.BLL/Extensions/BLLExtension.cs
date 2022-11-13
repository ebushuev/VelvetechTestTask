using Microsoft.Extensions.DependencyInjection;
using TodoApi.BLL.Interfaces;
using TodoApi.BLL.Services;

namespace TodoApi.BLL
{
    public static class BLLExtensions
    {
        public static void AddBLL(this IServiceCollection services)
        {
            services.AddTransient<ITodoItemService, TodoItemService>();
        }
    }
}