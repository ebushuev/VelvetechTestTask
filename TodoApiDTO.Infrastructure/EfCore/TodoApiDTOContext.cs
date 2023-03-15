using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Domain;

namespace TodoApiDTO.Infrastructure.EfCore
{
    public class TodoApiDTOContext : DbContext
    {
        public TodoApiDTOContext(DbContextOptions<TodoApiDTOContext> options)
           : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoItemDataConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
