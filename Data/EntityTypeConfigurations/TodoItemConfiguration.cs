using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.EntityTypeConfigurations
{
    internal class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired(true);

            builder.Property(p => p.IsComplete)
                .IsRequired(true);

            builder.HasData
            (
            new TodoItem
            {
                Id = Guid.NewGuid(),
                Name = "Купить хлебушек!",
                IsComplete = false
            },
            new TodoItem
            {
                Id = Guid.NewGuid(),
                Name = "Проснутся, улыбнутся",
                IsComplete = true
            },
            new TodoItem
            {
                Id = Guid.NewGuid(),
                Name = "Делать проект!",
                IsComplete = true
            },
            new TodoItem
            {
                Id = Guid.NewGuid(),
                Name = "Позвонить на работу",
                IsComplete = false
            });
        }
    }
}
