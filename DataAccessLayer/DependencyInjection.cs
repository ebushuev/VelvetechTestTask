using Microsoft.Extensions.DependencyInjection;
using TodoApi.DataAccessLayer.Abstract;
using TodoApi.DataAccessLayer.EntityFramework;

namespace TodoApi.DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemDal, EFTodoItemRepository>();
            return services;
        }
    }
}
