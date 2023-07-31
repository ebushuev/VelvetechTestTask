using Microsoft.EntityFrameworkCore;
using Todo.Core.Common;

namespace Todo.DataAccess.Repositories
{
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        private readonly DbContext _dbContext;

        protected BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetQuery()
        {
            return _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(entity, cancellationToken);
        }

        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }

        public DbContext Get_dbContext()
        {
            return _dbContext;
        }

        public async Task<T> FindByIdAsync(TKey id, CancellationToken cancellationToken, bool noTracking = false)
        {
            var result = await _dbContext.FindAsync<T>(new object[] { id }, cancellationToken);

            if (noTracking)
            {
                _dbContext.Entry(result).State = EntityState.Detached;
            }

            return result;
        }

        public async Task<T[]> GetAllAsync(CancellationToken cancellationToken, bool noTracking = false)
        {
            var queryableResult = _dbContext.Set<T>().AsQueryable();
            queryableResult = noTracking ? queryableResult.AsNoTracking() : queryableResult;

            return await queryableResult.ToArrayAsync(cancellationToken);
        }

        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        }
    }
}
