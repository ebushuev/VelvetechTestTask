using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.DataAccessLayer;

namespace TodoApiDTO.BusinessLogicLayer.Interfaces
{
    public interface IToDoService
    {
        Task DeleteToDoItemByIdAsync(TodoItemDTO modelDTO);
        Task<long> UpsertToDoItemsAsync(TodoItemDTO modelDTO);
        Task<TodoItemDTO> GetToDoItemByIdAsync(long id);
        Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync();

    }
}
