using System.Linq.Expressions;
using Velvetech.MyTodoApp.Application.DTOs;
using Velvetech.TodoApp.Domain.Entities;

namespace Velvetech.MyTodoApp.Application.Services.Abstractions
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemReadDto>> GetTodoItemsAsync(Expression<Func<TodoItemEntity, bool>> predicate);
        Task<IEnumerable<TodoItemReadDto>> GetTodoItemsAsync();
        Task<TodoItemReadDto> GetTodoItemAsync(Expression<Func<TodoItemEntity, bool>> predicate);
        Task<TodoItemReadDto> AddTodoItemAsync(TodoItemCreateDto todoItemDto);
        Task<TodoItemReadDto> UpdateTodoItemAsync(TodoItemUpdateDto todoItemDto);
        Task<bool> DeleteTodoItemAsync(Guid id);
    }
}
