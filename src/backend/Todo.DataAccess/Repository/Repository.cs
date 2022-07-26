using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.DataAccess.Data;
using Todo.DataAccess.Repository.IRepository;
using Todo.Domain.Common;

namespace Todo.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly TodoContext todoContext;

        public Repository(TodoContext todoContext)
        {
            this.todoContext = todoContext;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await todoContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(long id)
        {
            return await todoContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> CreateAsync(T entity)
        {
            ValidateItem(entity);
            todoContext.Set<T>().Add(entity);
            await todoContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await todoContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException();
            }
            todoContext.Set<T>().Remove(entity);
            await todoContext.SaveChangesAsync();
        }

        private static void ValidateItem(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
        }
    }
}