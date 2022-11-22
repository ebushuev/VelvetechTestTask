using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Interfaces
{
    public interface IRepository<T, TId> 
        where T : class, IDomainModel<TId>
    {
        public Task AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        public Task<List<T>> GetManyAsync(Expression<Func<T, bool>> expression);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        public Task<List<T>> GetAllAsync();

        public Task DeleteAsync(Expression<Func<T, bool>> expression);
    }
}
