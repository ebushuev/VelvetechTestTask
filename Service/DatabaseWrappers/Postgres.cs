using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApiDTO.DAL;
using TodoApiDTO.DAL.Entities;
using TodoApiDTO.Models;
using TodoApiDTO.ServiceInterfaces.DatabaseWrappers;

namespace TodoApiDTO.Service.DatabaseWrappers;

public class Postgres : IDataBase
{
    private readonly ILogger<Postgres> _logger;
    private readonly TodoContext _context;

    public Postgres(ILogger<Postgres> logger, TodoContext context)
    {
        _logger = logger;
        _context = context;
    }

   private static Func<TodoContext, long, Task<TodoItemEntity?>> GetCompiledTodoItem =
        EF.CompileAsyncQuery((TodoContext context, long id) =>
            context.TodoItems.FirstOrDefault(x => x.Id == id)
        );

    public async Task<TodoItemEntity?> GetAsync(long id, CancellationToken token)
    {
        var result = await GetCompiledTodoItem(_context, id);

        _logger.LogInformation("TodoItem {TodoItemId} was successfully received", id.ToString());
        
        return result;
    }

    
    private static Func<TodoContext, IAsyncEnumerable<TodoItemEntity>> GetCompiledTodoItems =
        EF.CompileAsyncQuery((TodoContext context) =>
            context.TodoItems
        );
    
    public async Task<List<TodoItemEntity>> GetListAsync(CancellationToken token)
    {
        var result = new List<TodoItemEntity>();

        await foreach (var item in GetCompiledTodoItems(_context))
        {
            result.Add(item);
        }

        _logger.LogInformation("TodoItems were successfully received count [{Count}]", result.Count.ToString());
        
        return result;
    }

    public async Task<TodoItemEntity?> CreateAsync(TodoItemCreateModel model, CancellationToken token)
    {
        var entity = new TodoItemEntity
        {
            Name = model.Name,
            IsComplete = false,
            Secret = model.Secret
        };
        
        var entry = await _context.TodoItems.AddAsync(entity, token);
        try
        {
            if (await _context.SaveChangesAsync(token) > 0)
                _logger.LogInformation("TodoItem {TodoItemName} was updated", entity.Name);
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Couldn't create TodoItem | ExceptionType {ExceptionType} | Exception {Exception}",
                e.GetType().Name, e.Message);
            return null;
        }
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(TodoItemUpdateModel model, CancellationToken token)
    {
        var result = await _context.TodoItems
            .Where(x => x.Id == model.Id)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(x => x.Name, model.Name)
                , token) > 0;

        if (result)
        {
            _logger.LogInformation("TodoItem {TodoItemName} was updated", model.Name);
        } else
        {
            _logger.LogWarning("TodoItem {TodoItemName} was not updated", model.Name);
        }

        return result;
    }
    
    public async Task<bool> SetCompleted(long id, CancellationToken token)
    {
        var result = await _context.TodoItems
            .Where(x => x.Id == id && x.IsComplete == false)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(x => x.IsComplete, true)
                , token) > 0;

        if (result)
        {
            _logger.LogInformation("TodoItem {TodoItemId} was set completed", id);
        }else
        {
            _logger.LogWarning("TodoItem {TodoItemId} was not set completed", id);
        }

        return result;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken token)
    {
        var result = await _context.TodoItems.Where(x => x.Id == id && x.IsComplete == false)
            .ExecuteDeleteAsync(token) > 0;

        if (result)
        {
            _logger.LogInformation("TodoItem {TodoItemId} was deleted", id);
        }else
        {
            _logger.LogWarning("TodoItem {TodoItemId} was not deleted", id);
        }

        return result;
    }
}