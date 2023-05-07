using Microsoft.Extensions.DependencyInjection;
using Todo.Bll.Services;
using Todo.Common.ServiceInterfaces;

namespace Todo.Bll.Configuration
{
    public static class Initializer
    {
        public static IServiceCollection ConfigureBll(this IServiceCollection services)
        {           
            services.AddTransient<ITodoService, TodoService>();

            return services;
        }
    }
}
