using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated(); 
            context.Database.Migrate();
        }
    }
}
