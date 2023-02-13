using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
	public class TodoContext : DbContext
	{
		public TodoContext(DbContextOptions<TodoContext> options)
			: base(options)
		{
		}
		public DbSet<TodoItem> TodoItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);

			modelBuilder.Entity<TodoItem>().HasData(
				new TodoItem { Id = 1, IsComplete = false, Name = "TEST", Secret = "SECRET" },
				new TodoItem { Id = 2, IsComplete = false, Name = "TEST-2", Secret = "SECRET" },
				new TodoItem { Id = 3, IsComplete = true, Name = "TEST-3", Secret = "SECRET" },
				new TodoItem { Id = 4, IsComplete = false, Name = "TEST-4", Secret = "SECRET" },
				new TodoItem { Id = 5, IsComplete = true, Name = "TEST-5", Secret = "SECRET" }
				);
		}
	}
}