using Data.Entities;
using System;
using System.Runtime.CompilerServices;

namespace Business.Models
{
    public static class MappingExtensions
    {
        public static TodoItemDto ToDto(this TodoItemEntity source)
        {
            return new TodoItemDto
            {
                Id = source.Id,
                Name = source.Name,
                IsComplete = source.IsComplete,
            };
        }

        public static TodoItemEntity ToEntity(this TodoItemDto source)
        {
            return new TodoItemEntity
            {
                Name = source.Name,
                IsComplete = source.IsComplete,
            };
        }
    }
}
