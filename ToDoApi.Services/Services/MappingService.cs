using System.Collections.Generic;
using TodoApi.Data.Models;
using TodoApi.Services.Models;
using TodoApi.Services.Services.Interfaces;

namespace TodoApi.Services.Services
{
    public class TodoItemMappingService : ITodoItemMappingService
    {
        public TodoItemDTO MapTodoItemToDTO(TodoItem todoItem)
        {
            return new TodoItemDTO
            {
                Id = todoItem.Id,
                IsComplete = todoItem.IsComplete,
                Name = todoItem.Name,
            };
        }

        public IEnumerable<TodoItemDTO> MapTodoItemToDTO(IReadOnlyCollection<TodoItem> todoItems)
        {
            foreach (var item in todoItems)
            {
                yield return MapTodoItemToDTO(item);
            }
        }
    }
}
