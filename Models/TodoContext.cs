using Microsoft.EntityFrameworkCore;
using TodoApi.Seeds;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedTodo.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
