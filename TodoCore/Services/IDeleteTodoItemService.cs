using System.Threading.Tasks;
using TodoCore.DTOs;

namespace TodoCore.Services
{
    public interface IDeleteTodoItemService
    {
        Task<TodoItemDTO> DeleteTodoItemAsync(long id);
    }
}
