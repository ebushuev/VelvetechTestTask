using System.Linq.Expressions;
using Velvetech.TodoApp.Domain.Entities;

namespace DataAccessLayer.Repository.Abstractions.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync();

        Task<IEnumerable<TEntity>> GetAsync();

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);
    }
}