using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TodoApiDTO.Application
{
    public static class SetupLayer
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(new Assembly[] { Assembly.GetExecutingAssembly() });

            return services;
        }
    }
}
