using System.Threading.Tasks;
using TodoCore.DTOs;

namespace TodoCore.Services
{
    public interface IAddTodoItemService
    {
        Task AddTodoItemAsync(TodoItemDTO todoItemDTO);
    }
}
