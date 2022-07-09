using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApi.Database;
using ToDoItems.DAL.Interfaces;

namespace TodoApiDTO.ToDoApi.DAL.Repositories.Implementation
{
    /// <summary>
    /// base items repository
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// database context
        /// </summary>
        private readonly ToDoContext _dbContext;

        /// <summary>
        /// set of items
        /// </summary>
        private readonly DbSet<T> _itemsSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BaseRepository(ToDoContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            _dbContext = dbContext;
            _itemsSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Find item by id
        /// </summary>
        /// <param name="id">item id</param>
        /// <returns></returns>
        public async Task<T> FindAsync(long id)
        {
            return await _itemsSet.FindAsync(id);
        }
       

        /// <summary>
        /// Create new item
        /// </summary>
        /// <param name="item">new item</param>
        public void Add(T item)
        {
            _itemsSet.Add(item);
        }

        /// <summary>
        /// Does item exist
        /// </summary>
        /// <param name="predicate">items predicate</param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return _itemsSet.Any(predicate);
            }

            return _itemsSet.Any();
        }

        /// <summary>
        /// Remove item from db
        /// </summary>
        /// <param name="id">item id</param>
        public void Delete(T item)
        {
            _itemsSet.Remove(item);
        }

        
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>all items list</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _itemsSet
               .Select(x => x)
               .ToListAsync();
        }

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
