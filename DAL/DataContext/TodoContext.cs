using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL.DataContext {
	public class TodoContext : DbContext {
		public TodoContext(DbContextOptions<TodoContext> options) : base(options) {
		}
		public DbSet<TodoItem> TodoItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<TodoItem>(item => {
				item.ToTable("TodoItems");
				item.HasKey(x => x.Id);
				item.Property(x => x.Id).ValueGeneratedOnAdd();
				item.Property(x => x.Name);
				item.Property(x => x.IsComplete);
				item.Property(x => x.Secret);
			});
		}
	}
}
