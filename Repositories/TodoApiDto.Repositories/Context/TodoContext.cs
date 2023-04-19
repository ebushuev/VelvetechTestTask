using Microsoft.EntityFrameworkCore;
using TodoApiDto.Repositories.Data;

namespace TodoApiDto.Repositories.Context
{
    public sealed class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("todo_item");
            modelBuilder.Entity<TodoItem>().HasKey(todoItem => todoItem.Id).HasName("id");
            modelBuilder.Entity<TodoItem>().Property(todoItem => todoItem.Name).HasColumnName("name");
            modelBuilder.Entity<TodoItem>().Property(todoItem => todoItem.Secret).HasColumnName("secret");
            modelBuilder.Entity<TodoItem>().Property(todoItem => todoItem.IsComplete).HasColumnName("is_complete").IsRequired();
        }
    }
}