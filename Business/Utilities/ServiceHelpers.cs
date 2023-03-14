using Microsoft.Extensions.DependencyInjection;
using TodoApi.Models;

namespace TodoApiDTO.Services
{
    public static class ServiceHelpers
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
        }
    }
}
