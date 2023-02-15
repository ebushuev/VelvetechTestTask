using Data.Entities;
using System;
using System.Runtime.CompilerServices;

namespace Business.Models
{
    public static class MappingExtensions
    {
        public static TodoItemDTO ToDto(this TodoItemEntity source)
        {
            return new TodoItemDTO
            {
                Id = source.Id,
                Name = source.Name,
                IsComplete = source.IsComplete,
            };
        }

        public static TodoItemEntity ToEntity(this TodoItemDTO source)
        {
            return new TodoItemEntity
            {
                Name = source.Name,
                IsComplete = source.IsComplete,
            };
        }
    }
}
