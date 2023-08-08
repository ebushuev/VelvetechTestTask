using Microsoft.EntityFrameworkCore;
using TodoApi.DAL.Entities;

namespace TodoApi.DAL.EF
{
    public class TodoContext : DbContext
    {
        public virtual DbSet<TodoItem> TodoItems { get; set; }

        public TodoContext()
        {
            
        }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem() { Id = 1, Name = "Finish test task", IsComplete = false },
                new TodoItem() { Id = 2, Name = "Take a break", IsComplete = true },
                new TodoItem() { Id = 3, Name = "Walk little bit", IsComplete = false },
                new TodoItem() { Id = 4, Name = "Learn React", IsComplete = false },
                new TodoItem() { Id = 5, Name = "Practice", IsComplete = true }
            );
        }
    }
}