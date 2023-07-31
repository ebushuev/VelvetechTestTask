using Microsoft.EntityFrameworkCore;
using Todo.Core.Business.TodoItem.Entities;

namespace Todo.DataAccess
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}