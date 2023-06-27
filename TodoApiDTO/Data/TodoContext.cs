using Microsoft.EntityFrameworkCore;

namespace TodoApiDTO.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext() { }


        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TodoItem> TodoItems { get; set; }
    }
}