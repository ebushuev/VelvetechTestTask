using Microsoft.EntityFrameworkCore;
using Todo.DAL.Entities;

namespace Todo.DAL.DbContexts;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
}
