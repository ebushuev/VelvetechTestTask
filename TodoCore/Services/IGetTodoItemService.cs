using System.Threading.Tasks;
using TodoCore.DTOs;

namespace TodoCore.Services
{
    public interface IGetTodoItemService
    {
        Task<TodoItemDTO> GetTodoItemAsync(long id);
    }
}
