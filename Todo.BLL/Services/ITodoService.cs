using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.BLL;

namespace Todo.BLL.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDTO>> GetAsync();
        Task<TodoItemDTO> GetAsync(long id);
        Task<long> CreateAsync(TodoItemDTO item);
        Task UpdateAsync(long id, TodoItemDTO item);
        Task DeleteAsync(long id);
    }
}
