using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TodoItemsRepository : ITodoItemsRepository
{
    private readonly TodoAppDbContext _context;

    public TodoItemsRepository(TodoAppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets every TodoItem
    /// </summary>
    /// <returns>Collection of TodoItems</returns>
    public async Task<IEnumerable<TodoItem>> GetAll()
    {
        return await _context.TodoItems.ToListAsync();
    }

    /// <summary>
    /// Gets every TodoItem
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="pageSize">Items per page</param>
    /// <returns>Collection of TodoItems</returns>
    public async Task<IEnumerable<TodoItem>> GetAllPaginated(int page = 1, int pageSize = 10)
    {
        var skip = (page - 1) * pageSize;
        return await _context.TodoItems
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Gets a specific TodoItem by its ID
    /// </summary>
    /// <param name="todoItemId">TodoItem ID</param>
    /// <returns>TodoItem</returns>
    public async Task<TodoItem> GetById(long todoItemId)
    {
        return await _context.TodoItems.FindAsync(todoItemId)
               ?? throw new EntityNotFoundException($"Todo Item with ID [{todoItemId}] does not exist.");
    }

    /// <summary>
    /// Creates a new TodoItem
    /// </summary>
    /// <param name="todoItem">New TodoItem</param>
    /// <returns>Created TodoItem</returns>
    public async Task<TodoItem> CreateTodoItem(TodoItem todoItem)
    {
        await _context.TodoItems.AddAsync(todoItem);
        await _context.SaveChangesAsync();

        return todoItem;
    }

    /// <summary>
    /// Updates an existing TodoItem
    /// </summary>
    /// <param name="todoItemId">TodoItem ID</param>
    /// <param name="todoItem">Updated TodoItem data</param>
    /// <returns>Updated TodoItem</returns>
    public async Task<TodoItem> UpdateTodoItem(long todoItemId, TodoItem todoItem)
    {
        var existingTodoItem = await _context.TodoItems.FindAsync(todoItemId) 
                          ?? throw new EntityNotFoundException($"Todo Item with ID [{todoItemId}] does not exist.");

        existingTodoItem.Name = todoItem.Name;
        existingTodoItem.IsComplete = todoItem.IsComplete;

        await _context.SaveChangesAsync();
        return existingTodoItem;
    }

    public async Task<TodoItem> UpdateTodoItemSecret(long todoItemId, string secret)
    {
        var existingTodoItem = await _context.TodoItems.FindAsync(todoItemId) 
                               ?? throw new EntityNotFoundException($"Todo Item with ID [{todoItemId}] does not exist.");

        existingTodoItem.Secret = secret;

        await _context.SaveChangesAsync();
        return existingTodoItem;
    }

    /// <summary>
    /// Deletes an existing TodoItem by ID
    /// </summary>
    /// <param name="todoItemId">TodoItem ID</param>
    /// <returns></returns>
    public async Task DeleteTodoItem(long todoItemId)
    {
        var todoItem = await _context.TodoItems.FindAsync(todoItemId) 
                       ?? throw new EntityNotFoundException($"Todo Item with ID [{todoItemId}] does not exist.");

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
    }
}