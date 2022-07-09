using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDoItems.DAL.Interfaces
{
    /// <summary>
    /// interface of repository with objects actions 
    /// </summary>

    public interface IRepository <T> where T : class
    {
        /// <summary>
        /// get item by id
        /// </summary>
        /// <param name="id">item id</param>
        /// <returns>found item</returns>
        Task<T> FindAsync(long id);

        /// <summary
        /// get list of all items
        /// </summary>
        /// <returns>list of all items</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// create new item
        /// </summary>
        /// <param name="item">new item</param>
        void Add(T item);

        /// <summary>
        /// delete item by id
        /// </summary>
        /// <param name="id">item id</param>
        void Delete (T item);

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();


        /// <summary>
        /// Does item exist
        /// </summary>
        /// <param name="predicate">items predicate</param>
        /// <returns>Does item exist</returns>
        bool Any(Expression<Func<T, bool>> predicate = null);

    }
}
