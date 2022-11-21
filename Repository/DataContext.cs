using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApiDTO.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<TodoItem> ToDoItems { get; set; }
    }
}
