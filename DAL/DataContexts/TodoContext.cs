using Microsoft.EntityFrameworkCore;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.DAL.DataContexts
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