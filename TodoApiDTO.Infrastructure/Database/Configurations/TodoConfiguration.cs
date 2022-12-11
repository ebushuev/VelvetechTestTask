using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApiDTO.Domain.Todo;

namespace TodoApiDTO.Infrastructure.Database.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.ToTable("todoitems");
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
