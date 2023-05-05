using System.Collections.Generic;
using System.Threading.Tasks;
using TodoModels.Models;

namespace TodoIData.IServices
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDTO>> GetAllAsync();
        Task<TodoItemDTO> GetByIdAsync(long id);
        Task AddAsync(TodoItemDTO accountDto);
        Task UpdateAsync(TodoItemDTO todoItemDTO);
        Task DeleteAsync(long id);
    }
}
