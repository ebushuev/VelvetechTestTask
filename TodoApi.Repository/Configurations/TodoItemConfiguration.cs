using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApiRepository.Models;

namespace TodoApiRepository.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(600);

            builder.Property(p => p.Secret)
                .HasMaxLength(1000);

            builder.Property(p => p.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
