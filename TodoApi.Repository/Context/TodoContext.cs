using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TodoApiRepository.Configurations;
using TodoApiRepository.Models;

namespace TodoApiRepository.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(TodoItemConfiguration).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                foreach(var entry in exception.Entries) 
                {
                    var databaseEntry = await entry.GetDatabaseValuesAsync();
                    if(databaseEntry == null) 
                    {
                        throw new NotImplementedException();
                    }
                }
                throw exception;
            }
        }
    }
}
