using DAL.DataContext;
using DAL.Entity;
using System.Collections.Generic;
using System.Linq;

namespace DataSeed {
	public static class DataSeeder {
		public static void SeedData(TodoContext context) {
			using var tran = context.Database.BeginTransaction();

			if (!context.TodoItems.Any()) {
				var todoItems = new List<TodoItem> {
					new TodoItem("item1", false),
					new TodoItem("item2", false),
					new TodoItem("item3", true)
				};

				context.TodoItems.AddRange(todoItems);

				context.SaveChanges();
			}
			tran.Commit();
		}
	}
}
