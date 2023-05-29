using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApiDto.Domain.Interfaces;

namespace TodoApiDto.Persistance
{
    internal static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<TEntity> MapBase<TEntity>(this EntityTypeBuilder<TEntity> builder, string tableName, string? schemaName = null)
       where TEntity : class, IEntity
        {
            builder.ToTable(tableName, schemaName);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newsequentialid()");

            return builder;
        }
    }
}
