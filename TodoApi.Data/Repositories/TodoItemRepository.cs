using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoApiDTO.Data.Models;

namespace TodoApiDTO.Data.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly TodoContext _context;

    public TodoItemRepository(TodoContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItem>> GetAll()
    {
        return await _context.TodoItems
            .ToListAsync();
    }

    public async Task<TodoItem?> Get(long id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public void Remove(TodoItem item)
        => _context.TodoItems.Remove(item);

    public async Task<bool> Exists(long id)
    {
        return await _context.TodoItems.AnyAsync(x => x.Id == id);
    }

    public async Task Add(TodoItem item)
    {
       await _context.TodoItems.AddAsync(item);
    }
}