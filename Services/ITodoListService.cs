using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoListService
    {
        Task<List<TodoItemDTO>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemAsync(long id);
        Task<bool> UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO);
        Task<bool> CreateTodoItemAsync(TodoItemDTO todoItemDTO);
        Task<bool> DeleteTodoItemAsync(long id);
        bool TodoItemExists(long id);
    }
}