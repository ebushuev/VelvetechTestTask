using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task<TEntity> GetByIdAsync(long id, IEnumerable<string> entitiesToInclude = null);
		Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates = null, IEnumerable<string> entitiesToInclude = null);
		Task DeleteAsync(TEntity entity);
		Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
		bool Exists(Expression<Func<TEntity, bool>> predicate);
	}
}
