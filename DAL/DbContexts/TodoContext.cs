using Microsoft.EntityFrameworkCore;
using Todo.DAL.Entities;

namespace Todo.DAL.DbContexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
