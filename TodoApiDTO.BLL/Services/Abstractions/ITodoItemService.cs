using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.BLL.DTOs.TodoItems;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.BLL.Services.Abstractions
{
    public interface ITodoItemService : IBaseService<TodoItem>
    {
        Task<CreateTodoItemResponseDTO> AddAsync(CreateTodoItemRequestDTO requestDTO);
        Task UpdateAsync(long id, UpdateTodoItemRequestDTO requestDTO);
        Task DeleteAsync(long id);
        Task<IEnumerable<TodoItemResponseDTO>> GetAllAsync();
        Task<TodoItemResponseDTO> GetTodoItemByIdAsync(long id);
    }
}
