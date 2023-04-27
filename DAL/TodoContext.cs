using Microsoft.EntityFrameworkCore;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.DAL;

public class TodoContext : DbContext
{
    public TodoContext() { }
    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    public DbSet<TodoItemEntity> TodoItems { get; set; }
}
