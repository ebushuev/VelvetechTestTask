using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Dtos;


namespace TodoApiDTO.Core.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync();
        Task<TodoItemDTO> GetTodoItemByIdAsync(long id);
        Task UpdateTodoItemAsync(TodoItemDTO todoItemDTO);
        Task DeleteTodoItemAsync(TodoItemDTO todoItemDTO);
        Task<TodoItem> CreateTodoItemAsync(TodoItemDTO todoItemDTO);
        TodoItemDTO ItemToDTO(TodoItem todoItem);
    }
}