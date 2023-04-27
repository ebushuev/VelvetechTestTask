using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.BusinessLayer.Abstract
{
    public interface ITodoItemsService
    {
        public Task<TodoItemDTO> CreateTodoAsync(TodoItemDTO item);
        public Task<TodoItemDTO> GetTodoAsync(long id);
        public Task<List<TodoItemDTO>> GetTodoList();
        public Task UpdateTodoAsync(long id, TodoItemDTO item);
        public Task DeleteTodoAsync(long id);

    }
}
