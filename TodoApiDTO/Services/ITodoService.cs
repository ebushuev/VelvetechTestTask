using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.Services
{
    public interface ITodoService
    {
        Task<TodoItemDTO> Create(TodoItemDTO todoItemDTO);
        Task<bool> Delete(long id);
        Task<List<TodoItemDTO>> Get();
        Task<TodoItemDTO?> Get(long id);
        Task<bool> Update(long id, TodoItemDTO todoItemDTO);
    }
}