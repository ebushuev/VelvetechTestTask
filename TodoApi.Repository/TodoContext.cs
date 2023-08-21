namespace TodoApi.Repository
{
    using Microsoft.EntityFrameworkCore;
    using TodoApi.ObjectModel.Models;
    using TodoApi.Repository.Configurations;

    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoItemsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}