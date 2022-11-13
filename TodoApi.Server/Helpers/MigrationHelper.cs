using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApiRepository.Context;

namespace TodoApi.Server.Helpers
{
    public sealed class MigrationHelper
    {
        /// <summary>
        /// Initiate migration process
        /// </summary>
        /// <param name="host">Host builder</param>
        public void Migrate(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
                db.Database.Migrate();
            }
            
        }
    }
}
