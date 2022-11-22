using Microsoft.Extensions.DependencyInjection;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Services;

namespace TodoApi.Domain.DI
{
    public static class Setup
    {
        public static void RegisterDomain(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
        }
    }
}
