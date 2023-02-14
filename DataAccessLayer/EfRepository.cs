using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using TodoApiDTO.Core.DataAccess;


namespace TodoApiDTO.DataAccessLayer
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly TodoDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(TodoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAllAsync()
           => await _dbSet.ToListAsync();


        public async Task<IEnumerable<T>> GetAllByConditionAsync(Expression<Func<T, bool>> predicate,
                                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                            int? top = null,
                                                            int? skip = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (top.HasValue)
            {
                query = query.Take(top.Value);
            }

            return await query.ToListAsync();
        }


        public async Task<T> GetByIdAsync(long id)
            => await _dbSet.FindAsync(id);


        public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.FirstOrDefaultAsync(predicate);


        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }


        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            var entities = _dbSet.Where(filter);
            _dbSet.RemoveRange(entities);

            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}