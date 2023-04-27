using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.BusinessLayer.Abstract;
using TodoApi.BusinessLayer.Concrete;

namespace TodoApi.BusinessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemsService, TodoItemsService>();
            services.AddAutoMapper(typeof(DependencyInjection));
            return services;
        }
    }
}
