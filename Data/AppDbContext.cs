using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApi.Models;

namespace TodoApi.Data{
    public class AppDbContext : DbContext{
        private readonly IConfiguration _config;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
        : base(options)
        {
            _config = config;
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conStrBuilder = new SqlConnectionStringBuilder(
                    _config.GetConnectionString("ConnectionToDb"));
            conStrBuilder.Password = _config.GetValue<string>("DbPassword");
            var connection = conStrBuilder.ConnectionString;
            optionsBuilder.UseSqlServer(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
  

    }
}