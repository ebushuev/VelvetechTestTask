using Microsoft.EntityFrameworkCore;

namespace TodoApi.DataLayer.Config
{
    public static class EnityConfigExtensions
    {
        public static ModelBuilder AddEntities(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnityConfigExtensions).Assembly);

            return modelBuilder;
        }
    }
}