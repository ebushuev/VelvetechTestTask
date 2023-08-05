using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo.DAL.DbContexts;

namespace Todo.DAL;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly TodoDbContext _dbContext;

    public Repository(TodoDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<T>> GetAll(bool trackChanges)
    {
        IQueryable<T> query = !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        return await query.ToListAsync();
    }

    public async Task<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        IQueryable<T> query = !trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking()
            : _dbContext.Set<T>().Where(expression);

        return await query.FirstOrDefaultAsync();
    }

    public void Create(T entity) => _dbContext.Set<T>().Add(entity);

    public void Update(T entity) => _dbContext.Set<T>().Update(entity);

    public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}
