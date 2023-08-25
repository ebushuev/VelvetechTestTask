using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem> GetTodoItem(long id);
        Task<bool> UpdateTodoItem(long id, TodoItemDTO Item);
        Task<TodoItem> CreateTodoItem(TodoItemDTO Item);
        Task<bool> DeleteTodoItem(long id);
        Task<bool> TodoItemExists(long id);

    }
}