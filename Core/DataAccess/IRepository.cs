using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;


namespace TodoApiDTO.Core.DataAccess
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllByConditionAsync(Expression<Func<T, bool>> predicate,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                    int? top = null,
                                                    int? skip = null);
        Task<T> GetByIdAsync(long id);
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteManyAsync(Expression<Func<T, bool>> filter);
    }
}