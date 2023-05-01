using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApiDTO.DataAccessLayer.Entity;

namespace TodoApiDTO.DataAccessLayer.Repositories
{
    public interface IToDoRepository<T> where T : EntityBase
    {
        Task<T> AddAsync(T entity);
        Task<long> UpdateAsync(T entity);
        Task<long> DeleteAsync(T entity);
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllByConditionsAsync(Expression<Func<T, bool>> predicate,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                    int? top = null,
                                                    int? skip = null);
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
