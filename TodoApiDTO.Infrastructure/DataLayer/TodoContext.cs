using Microsoft.EntityFrameworkCore;
using TodoApiDTO.ToDoApiModels.Models;

namespace TodoApiDTO.TodoApiDTO.Infrastructure.DataLayer
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}