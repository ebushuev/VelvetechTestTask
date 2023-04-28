using Microsoft.EntityFrameworkCore;
using TodoCore.Data.Entities;

namespace TodoInfrastructure.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
