using System;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApi.DataLayer.Dto
{
    public static class DtoExtensions
    {
        public static IServiceCollection AddProfilesAutoMapper(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            return serviceProvider;
        }
    }
}