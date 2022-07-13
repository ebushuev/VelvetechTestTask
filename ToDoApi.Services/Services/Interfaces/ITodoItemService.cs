using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Services.Models;

namespace TodoApi.Services.Services.Interfaces
{
    public interface ITodoItemService
    {
        public Task<IEnumerable<TodoItemDTO>> GetAsync();

        public Task<TodoItemDTO> GetAsync(long id);

        public Task UpdateAsync(long id, TodoItemDTO item);
        public Task<TodoItemDTO> CreateAsync(TodoItemDTO item);
        public Task DeleteAsync(long id);
    }
}
