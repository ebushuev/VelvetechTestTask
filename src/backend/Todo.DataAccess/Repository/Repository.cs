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

        public async Task<long> CreateAsync(T item)
        {
            ValidateItem(item);
            todoContext.Set<T>().Add(item);
            await todoContext.SaveChangesAsync();
            return item.Id;
        }

        public async Task DeleteAsync(long id)
        {
            var todoItem = await todoContext.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException();
            }
            todoContext.TodoItems.Remove(todoItem);
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