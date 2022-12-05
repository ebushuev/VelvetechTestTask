using Microsoft.EntityFrameworkCore;
using ToDo.DAL.EntityTypeConfigurations;
using ToDo.DAL.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.DAL
{
    public class TodoDbContext : DbContext, ITodoDbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }
        public DbSet<ToDoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToDoConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}