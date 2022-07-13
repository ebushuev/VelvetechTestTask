using Microsoft.EntityFrameworkCore;
using TodoApi.Data.Models;

namespace TodoApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .Property(b => b.IsComplete)
                .HasDefaultValue(false);
        }
    }
}
