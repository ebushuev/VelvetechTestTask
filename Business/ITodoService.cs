using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface ITodoService
    {
        Task<TodoItemDTO> CreateItem(TodoItemDTO item);
        Task DeleteItem(long id);
        Task<TodoItemDTO> GetItem(long id);
        Task<IEnumerable<TodoItemDTO>> GetItems();
        Task UpdateItem(long id, TodoItemDTO todoItem);
    }
}