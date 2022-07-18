using Microsoft.Extensions.DependencyInjection;
using Todo.BLL.Services;

namespace Todo.BLL
{
    public static class BLLExtensions
    {
        public static void AddBL(this IServiceCollection services)
        {
            services.AddTransient<ITodoService, TodoService>();
        }
    }
}
