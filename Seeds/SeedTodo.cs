using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Seeds 
{
	public static class SeedTodo
	{
		public static void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<TodoItem>()
				.HasData
				(
					new TodoItem { Id = 1, Name = "Todo #1", IsComplete = true, Secret = "Secret A" },
					new TodoItem { Id = 2, Name = "Todo #2", IsComplete = false, Secret = "Secret B" }
				);
		}
	}
}
