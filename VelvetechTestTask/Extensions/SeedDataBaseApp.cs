using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApiDto.Application.Interfaces;
using TodoApiDto.Persistance;
using TodoApiDTO.DbSeeds;

namespace TodoApiDTO.Extensions
{
    public static class SeedDataBaseApp
    {
        internal static IHost SeedDataBase(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ITodoApiDtoDbContext>() as TodoApiDtoDbContext;

                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }

                var configuration = services.GetRequiredService<IConfiguration>();

                TodoApiDbContextSeeds.SeedSampleDataAsync(context, configuration).Wait();
           
            return host;
        }
    }
}
