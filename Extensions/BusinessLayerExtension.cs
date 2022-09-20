using Microsoft.Extensions.DependencyInjection;
using TodoApiDTO.BusinessLayer;
using TodoApiDTO.DataAccessLayer;

namespace TodoApiDTO.Extensions
{
    public static class BusinessLayerExtension
    {
        public static void ConfigureBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IBusinessLayer, BusinessLayerImplementation>();
        }
    }
}
