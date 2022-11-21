using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Models;

namespace TodoApiDTO.Helpers
{
    public class Mapper
    {
        public static TodoItemDTO ToDto(TodoItem model)
        {
            return new TodoItemDTO()
            {
                Id = model.Id,
                IsComplete = model.IsComplete,
                Name = model.Name,
            };
        }
        public static TodoItem ToModel(TodoItemDTO dto)
        {
            return new TodoItem()
            {
                IsComplete = dto.IsComplete,
                Name = dto.Name,
            };
        }

        public static TodoItem ToModel(CreateTodoItemRequest dto)
        {
            return new TodoItem()
            {
                IsComplete = dto.IsComplete,
                Name = dto.Name,
                Secret = dto.Secret
            };
        }

        public static TodoItem ToModel(UpdateTodoItemRequest dto)
        {
            return new TodoItem()
            {
                Id = dto.Id,
                IsComplete = dto.IsComplete,
                Name = dto.Name,
                Secret = dto.Secret
            };
        }
    }
}
