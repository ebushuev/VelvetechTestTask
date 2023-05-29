using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TodoApiDto.Domain.Entities;

namespace TodoApiDto.Persistance
{
    public class ItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.MapBase("Items");
        }
    }
}
