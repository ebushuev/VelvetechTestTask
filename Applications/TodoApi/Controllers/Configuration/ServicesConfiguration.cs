using Microsoft.Extensions.DependencyInjection;
using TodoApiDto.Services;
using TodoApiDto.Services.Interfaces;

namespace TodoApi.Controllers.Configuration
{
    public class ServicesConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ITodoService, TodoService>();
        }
    }
}