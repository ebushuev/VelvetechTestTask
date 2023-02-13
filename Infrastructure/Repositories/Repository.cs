using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		protected readonly TodoContext TodoContext;
		protected Repository(TodoContext todoContext) => TodoContext = todoContext;
		public async Task AddAsync(TEntity entity) => await TodoContext.Set<TEntity>().AddAsync(entity);
		public async Task UpdateAsync(TEntity entity) => await Task.Run(() => { TodoContext.Set<TEntity>().Update(entity); });
		public async Task<TEntity> GetByIdAsync(long id, IEnumerable<string> entitiesToInclude = null) => await TodoContext.Set<TEntity>().Include(entitiesToInclude)
																												 .FirstOrDefaultAsync(e => e.Id == id);
		public async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates = null,
															IEnumerable<string> entitiesToInclude = null) => await TodoContext.Set<TEntity>().Filter(predicates)
																													 .Include(entitiesToInclude)
																													 .ToListAsync();
		public async Task DeleteAsync(TEntity entity) => await Task.Run(() => { Delete(entity); });
		public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate) => await TodoContext.Set<TEntity>().AnyAsync(predicate);
		public bool Exists(Expression<Func<TEntity, bool>> predicate) => TodoContext.Set<TEntity>().Any(predicate);
		private void Delete(TEntity entity) => TodoContext.Set<TEntity>().Remove(entity);
	}
}