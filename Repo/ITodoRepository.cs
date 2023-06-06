using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Models;

namespace TodoApiDTO.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemAsync(long id);
        void CreateTodoItem(TodoItem todoItem);
        void DeleteTodoItem(TodoItem todoItem);
        Task SaveChangesAsync();
        bool TodoItemExists(long id);
    }
}