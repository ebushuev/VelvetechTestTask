using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.DAL.Contexts;
using TodoApiDTO.DAL.Entities.Abstractions;
using TodoApiDTO.DAL.Repositories.Abstractions;

namespace TodoApiDTO.DAL.Repositories.Implementations
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TodoDbContext dbContext) => _dbSet = dbContext.Set<TEntity>();

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public async Task<TEntity> GetByIdAsync(long id) => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public void Update(TEntity entity) => _dbSet.Update(entity);

        public void Delete(TEntity entity) => _dbSet.Remove(entity);
    }
}
