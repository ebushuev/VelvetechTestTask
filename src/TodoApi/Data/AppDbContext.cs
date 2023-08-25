using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApi.Models;

namespace TodoApi.Data{
    public class AppDbContext : DbContext{

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            
        }

        public virtual DbSet<TodoItem> TodoItems { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
  

    }
}