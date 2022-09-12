using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain;

namespace Todo.Domain.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetItemsAsync();
        Task<TodoItem> FindItemAsync(long id);
        Task UpdateTodoItemAsync(TodoItem todoItem);
        Task<TodoItem> CreateItemAsync(TodoItem todoItem);
        Task DeleteItemAsync(long id);
    }
}
