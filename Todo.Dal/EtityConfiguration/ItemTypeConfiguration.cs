using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Common.Models.Domain;

namespace Todo.Dal.EtityConfiguration
{
    internal class ItemTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.Id);

            builder.Property(s => s.IsComplete).HasDefaultValue(false).IsRequired();
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.CreatedOn).IsRequired();
        }
    }
}
