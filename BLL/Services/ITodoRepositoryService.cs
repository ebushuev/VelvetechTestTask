using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TodoApi.BLL.Dto;

namespace TodoApi.BLL.Services {
    public interface ITodoRepositoryService {
        Task<Result<List<TodoItemDTO>>> GetTodoItemsAsync();
        Task<Result<TodoItemDTO>> GetTodoItemAsync(long id);
        Task<Result> UpdateTodoItemAsync(long id, TodoItemDTO todoItemDto);
        Task<Result<TodoItemDTO>> CreateTodoItemAsync(TodoItemDTO todoItemDto);
        Task<Result> DeleteTodoItem(long id);
    }
}