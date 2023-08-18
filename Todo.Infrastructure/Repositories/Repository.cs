using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.DbContexts;
using Todo.Infrastructure.Interfaces;

namespace Todo.Infrastructure.Repositories;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly TodoDbContext _context;

    public Repository(TodoDbContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public async Task<T> Read(Expression<Func<T, bool>> condition)
    {
        return await _context.Set<T>()
            .Where(condition)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
}