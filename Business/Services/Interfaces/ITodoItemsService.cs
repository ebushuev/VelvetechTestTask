using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Dtos;

namespace Business.Services.Interfaces
{
    public interface ITodoItemsService
    {
        public Task<IEnumerable<TodoItemDto>> GetTodoItems();
        public Task<TodoItemDto> GetTodoItem(long id);
        public Task UpdateTodoItem(long id, TodoItemDto todoItemDto);
        public Task<long> CreateTodoItem(TodoItemDto todoItemDto);
        public Task DeleteTodoItem(long id);
    }
}