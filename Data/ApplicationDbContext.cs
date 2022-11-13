using Application.Interfaces;
using Data.EntityTypeConfigurations;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TodoItemConfiguration());
            base.OnModelCreating(builder);
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
