using Microsoft.EntityFrameworkCore;
using TodoApi.BusinessLayer.Models;
using TodoApi.BusinessLayer.Repositories;

namespace TodoApi.Storage.Contexts
{
    public class TodoContext : DbContext, ITodoItemRepository
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