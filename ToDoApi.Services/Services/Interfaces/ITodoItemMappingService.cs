using System.Collections.Generic;
using TodoApi.Data.Models;
using TodoApi.Services.Models;

namespace TodoApi.Services.Services.Interfaces
{
    public interface ITodoItemMappingService
    {
        public TodoItemDTO MapTodoItemToDTO(TodoItem todoItem);
        public IEnumerable<TodoItemDTO> MapTodoItemToDTO(IEnumerable<TodoItem> todoItems);
    }
}
