using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations
{
	public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
	{
		public void Configure(EntityTypeBuilder<TodoItem> builder)
		{
			builder.ToTable(nameof(TodoItem));

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired();
		}
	}
}