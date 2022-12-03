using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TodoApiDTO.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {

        IQueryable<T> GetAll();
        Task<T> GetAsync(long id);
        void Update(T item);
        IEnumerable<T> Find(Func<T, bool> predicate);
        Task<EntityEntry<T>> CreateAsync(T item);
        void Delete(T item);
        Task<int> SaveChangesAsync();
    }
}
