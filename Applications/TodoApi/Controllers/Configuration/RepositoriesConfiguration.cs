using Microsoft.Extensions.DependencyInjection;
using TodoApiDto.Repositories;
using TodoApiDto.Repositories.Interfaces;

namespace TodoApi.Controllers.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ITodoRepository, TodoRepository>();
        }
    }
}