using System.Collections.Generic;
using System.Threading.Tasks;
using Velvetech.Todo.Logic.Models;

namespace Velvetech.Todo.Logic
{
  public interface ITodoItemService
  {
    Task<TodoItemModel> InsertTodoItemAsync(TodoItemModel item);
    Task<TodoItemModel> GetTodoItemByIdAsync(long id);
    Task<TodoItemModel> UpdateTodoItemAsync(TodoItemModel item);
    Task<IEnumerable<TodoItemModel>> GetAllTodoItemsAsync();
    Task DeleteTodoItemAsync(long id);
  }
}
