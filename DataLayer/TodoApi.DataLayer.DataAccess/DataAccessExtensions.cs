using Microsoft.Extensions.DependencyInjection;

namespace TodoApi.DataLayer.DataAccess
{
    public static class DataAccessExtensions
    {
        public static IServiceCollection AddEntityServices<TEntity>(this IServiceCollection serviceCollection)
            where TEntity : class
        {
            serviceCollection.AddTransient<IEntityAccessService<TEntity>, EntityAccessService<TEntity>>();

            return serviceCollection;
        }
    }
}