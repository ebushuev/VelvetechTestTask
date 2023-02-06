using Microsoft.EntityFrameworkCore;
using TodoApi.DataLayer.Config;

namespace TodoApi.Infrastructure.DbContext
{
    public class TodoContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntities();
        }
    }
}