using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class TodoAppDbContext : DbContext
{
    public TodoAppDbContext(DbContextOptions options) : base(options)
    {}

    public DbSet<TodoItem> TodoItems { get; set; }
}