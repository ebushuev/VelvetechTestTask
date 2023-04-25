using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.BLL.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync();
        Task<TodoItemDTO> GetTodoItemAsync(long id);
        Task<TodoItemDTO> AddAsync(TodoItemDTO todoItemDTO);
        Task EditAsync(TodoItemDTO todoItemDTO);
        Task DeleteAsync(long id);
    }
}