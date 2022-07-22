using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Entities;

namespace TodoApiDTO.Context
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