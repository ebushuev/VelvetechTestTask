using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Todo.DAL.Repositories
{
    public abstract class BaseRepository<T> where T : class, IEntity
    {
        protected readonly TodoContext Db;

        public BaseRepository(TodoContext db)
        {
            Db = db;
        }

        public async Task<IList<T>> GetList()
        {
            return await Db.Set<T>().ToListAsync();
        }
    
        public async Task<T> Get(long id)
        {
            return await Db.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await Db.Set<T>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Db.Set<T>().AddRange(entities);
        }

        public void Delete(T entity)
        {
            Db.Entry(entity).State = EntityState.Deleted;
        }
        
        public void DeleteRange(IEnumerable<T> entities)
        {
            Db.Set<T>().RemoveRange(entities);
        }

        public async Task<bool> IsExists(long id)
        {
            return await Db.Set<T>().AnyAsync(x => x.Id == id);
        }

        public async Task<T> GetByIdAsync(long id, Func<IQueryable<T>, IQueryable<T>> includes = null, bool asNoTracking = false)
        {
            var query = Db.Set<T>().AsQueryable();

            if (includes != null)
                query = includes(query);

            if (asNoTracking)
                query.AsNoTracking();

            var entity = await query.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            return entity;
        }

        public async Task SaveChanges()
        {
            await Db.SaveChangesAsync();
        }
    }
}