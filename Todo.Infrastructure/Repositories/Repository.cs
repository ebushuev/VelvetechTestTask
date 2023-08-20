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
        T item = await _context.Set<T>()
            .Where(condition)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return item ?? Activator.CreateInstance<T>(); // Return an empty instance if item is null
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
        List<T> items = await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();

        return items;
    }
}