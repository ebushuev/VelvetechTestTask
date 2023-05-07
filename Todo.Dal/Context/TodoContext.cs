using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Todo.Common.Models.Domain;
using Todo.Dal.EtityConfiguration;

namespace Todo.Dal.Context
{
    internal class TodoContext : DbContext
    {
        public TodoContext()
        {
        }

        public TodoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Item { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("TodoDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemTypeConfiguration());
        }
    }
}
