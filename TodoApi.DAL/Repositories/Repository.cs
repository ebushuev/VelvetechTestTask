using Microsoft.EntityFrameworkCore;
using TodoApi.DAL.Models;
using TodoApi.DAL.Repositories.Contracts;

namespace TodoApi.DAL.Repositories;

public class Repository<DbC, T> : IRepository<T> 
    where DbC : DbContext 
    where T : BaseEntity, new()
{
    private readonly DbC _dbContext;

    public Repository(DbC todoContext)
    {
        _dbContext = todoContext;
    }
    
    private bool IsExists(long id) =>
        _dbContext.Set<T>().Any(e => e.Id == id);
    
    public async Task<T> Create(T item)
    {
        var obj = await _dbContext.Set<T>().AddAsync(item);  
        await _dbContext.SaveChangesAsync();  
        return obj.Entity;
    }

    public async Task<bool> Delete(long id)
    {
        if (!IsExists(id))
        {
            return false;
        }
        
        _dbContext.Set<T>().Remove(new T { Id = id });
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(T changedItem)
    {
        if (!IsExists(changedItem.Id))
        {
            return false;
        }

        try
        {
            _dbContext.Set<T>().Update(changedItem);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!IsExists(changedItem.Id))
        {
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<T>> GetAll()
    { 
        return _dbContext.Set<T>();
    }

    public async Task<T?> GetById(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }
}