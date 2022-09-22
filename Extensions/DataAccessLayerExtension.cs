using Microsoft.Extensions.DependencyInjection;
using TodoApiDTO.DataAccessLayer;

namespace TodoApiDTO.Extensions
{
    public static class DataAccessLayerExtension
    {
        public static void ConfigureDataAccessLayer(this IServiceCollection services)
        {
            services.AddScoped<IDataAccessLayer, DataAccessLayerImplementation>();
        }
    }
}
