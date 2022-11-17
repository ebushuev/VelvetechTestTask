using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.DAL.EntityConfigurations
{
    public class TodoItemConfiguration : BaseEntityConfiguration<TodoItem>
    {
        public override void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name)
                .HasMaxLength(512)
                .IsRequired(true);

            builder.Property(p => p.IsComplete)
                .IsRequired(true);

            builder.Property(p => p.Secret)
                .HasMaxLength(512)
                .IsRequired(false);
        }
    }
}
