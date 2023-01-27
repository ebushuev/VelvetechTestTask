using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static  class DomainServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<ITodoService, TodoService>();
            return services;
        }
    }
}
