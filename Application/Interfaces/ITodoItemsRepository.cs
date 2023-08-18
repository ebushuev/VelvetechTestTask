using Domain.Entities;

namespace Application.Interfaces;

public interface ITodoItemsRepository
{
    /// <summary>
    /// Gets every TodoItem
    /// </summary>
    /// <returns>Collection of TodoItems</returns>
    Task<IEnumerable<TodoItem>> GetAll();
    
    /// <summary>
    /// Gets every TodoItem and separates them by pages
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="pageSize">Items per page</param>
    /// <returns>Collection of TodoItems</returns>
    Task<IEnumerable<TodoItem>> GetAllPaginated(int page = 1, int pageSize = 10);

    /// <summary>
    /// Gets a specific TodoItem by its ID
    /// </summary>
    /// <param name="todoItemId">TodoItem ID</param>
    /// <returns>TodoItem</returns>
    Task<TodoItem> GetById(long todoItemId);

    /// <summary>
    /// Creates a new TodoItem
    /// </summary>
    /// <param name="todoItem">New TodoItem</param>
    /// <returns>Created TodoItem</returns>
    Task<TodoItem> CreateTodoItem(TodoItem todoItem);

    /// <summary>
    /// Updates an existing TodoItem
    /// </summary>
    /// <param name="todoItemId">TodoItem ID</param>
    /// <param name="todoItem">Updated TodoItem data</param>
    /// <returns>Updated TodoItem</returns>
    Task<TodoItem> UpdateTodoItem(long todoItemId, TodoItem todoItem);
    
    /// <summary>
    /// Updates TodoItem's secret
    /// </summary>
    /// <param name="todoItemId">TodoItem ID</param>
    /// <param name="secret">TodoItem Secret</param>
    /// <returns>Updated TodoItem</returns>
    Task<TodoItem> UpdateTodoItemSecret(long todoItemId, string secret);

    /// <summary>
    /// Deletes an existing TodoItem by ID
    /// </summary>
    /// <param name="todoItemId">TodoItem ID</param>
    /// <returns></returns>
    Task DeleteTodoItem(long todoItemId);
}