using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem> GetTodoItem(long id);
        Task UpdateTodoItem(long id, TodoItemDTO Item);
        Task CreateTodoItem(TodoItemDTO Item);
        Task DeleteTodoItem(long id);
        Task<bool> TodoItemExists(long id);

    }
}