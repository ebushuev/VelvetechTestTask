using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Core.Models;

namespace TodoApiDTO.Core.Services
{
    public interface ITodoService
    {
        Task<List<TodoItemDTO>> GetAllAsync();
        Task<TodoItemDTO> FindAsync(long id);
        Task<bool> GetIsExistAsync(long id);
        Task<TodoItemDTO> CreateAsync(TodoItemCreateDTO dto);
        Task UpdateAsync(TodoItemDTO dto);
        Task DeleteAsync(long id);
        Task<bool> GetNameIsUsedAsync(string name);
        Task<bool> GetNameIsUsedExceptOneAsync(long id, string name);
    }
}