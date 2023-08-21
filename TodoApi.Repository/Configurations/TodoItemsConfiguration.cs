namespace TodoApi.Repository.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TodoApi.ObjectModel.Models;

    internal sealed class TodoItemsConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        private const string TableName = "TodoItems";

        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(TableName);
        }
    }
}