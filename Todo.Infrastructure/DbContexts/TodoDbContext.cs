using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Entities;

namespace Todo.Infrastructure.DbContexts;

public class TodoDbContext : DbContext
{
    public TodoDbContext()
    {
    }
    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
}