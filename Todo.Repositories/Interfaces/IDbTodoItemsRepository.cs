using System.Collections.Generic;
using System.Threading.Tasks;
using Velvetech.Todo.Repositories.Entities;

namespace Velvetech.Todo.Repositories.Interfaces
{
  public interface IDbTodoItemsRepository
  {
    Task<DbTodoItem> GetTodoItemByIdAsync(long id);
    Task<DbTodoItem> InsertTodoItemAsync(DbTodoItem item);
    Task<DbTodoItem> UpdateTodoItemAsync(DbTodoItem item);
    Task<IEnumerable<DbTodoItem>> GetAllTodoItemsAsync();
    Task DeleteTodoItemAsync(long id);
  }
}
