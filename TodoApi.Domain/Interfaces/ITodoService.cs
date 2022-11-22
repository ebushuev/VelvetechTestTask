using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Interfaces
{
    public interface ITodoService
    {
        public Task<TodoItem> GetTodoItemByIdAsync(long id);

        public Task<List<TodoItem>> GetAllTodoItemsAsync();

        public Task<bool> IsTodoItemExistsAsync(long id);

        public Task AddTodoItemAsync(TodoItem item);

        public Task UpdateTodoItemAsync(TodoItem item);

        public Task DeleteTodoItemAsync(long id);
    }
}
