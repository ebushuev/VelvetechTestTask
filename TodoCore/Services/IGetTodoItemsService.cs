using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCore.DTOs;

namespace TodoCore.Services
{
    public interface IGetTodoItemsService
    {
        Task<List<TodoItemDTO>> GetTodoItemsAsync();
    }
}
