using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;
using TodoApi.Models;

namespace TodoApi.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T, TId> : IRepository<T, TId>
        where T : class, IDomainModel<TId>
    {
        readonly private TodoContext _context;

        public RepositoryBase(TodoContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<T> Set => _context.Set<T>();

        public async Task AddAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entitiesToDelete = await Set
                .Where(expression)
                .ToListAsync();

            foreach(var entityToDelete in entitiesToDelete)
                _context.Entry(entityToDelete).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            var result = await Set.AnyAsync(expression);

            return result;
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            var result = await Set
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);

            return result;
        }

        public async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> expression)
        {
            var result = await Set
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result = await Set
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
    }
}
