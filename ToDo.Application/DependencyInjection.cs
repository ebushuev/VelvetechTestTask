using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Mapping;
using ToDo.Application.Services;

namespace ToDo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemService, TodoItemService>();
            services.AddAutoMapper(typeof(AppMapperProfile));

            return services;
        }
    }
}