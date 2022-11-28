using Microsoft.EntityFrameworkCore;
using DAL.DataTransferObjects;

namespace DAL
{
    public class TodoContext : DbContext
    {
        public DbSet<ToDoItemDTO> ToDoItems { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
    }
}