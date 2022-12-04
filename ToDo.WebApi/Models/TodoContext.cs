using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Models;

namespace ToDo.WebApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> TodoItems { get; set; }
    }
}