using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
