using Microsoft.EntityFrameworkCore;
using TodoApi.BusinessLayer.Models;

namespace TodoApi.Storage.Contexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TodoItem>()
                .Property(t => t.Name)
                .HasField("_name")
                .IsRequired();

            modelBuilder.Entity<TodoItem>()
                .Property(t => t.IsComplete)
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<TodoItem>()
                .Property(t => t.Secret);
        }
    }
}