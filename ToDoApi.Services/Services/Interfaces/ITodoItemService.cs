using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Data.Models;
using TodoApi.Services.Models;

namespace TodoApi.Services.Services.Interfaces
{
    public interface ITodoItemService
    {
        public Task<IReadOnlyCollection<TodoItem>> GetAsync();

        public Task<TodoItem> GetAsync(long id);

        public Task<TodoItem> UpdateAsync(long id, TodoItemDTO item);
        public Task<TodoItem> CreateAsync(TodoItemDTO item);
        public Task DeleteAsync(long id);
    }
}
