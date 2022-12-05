using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApiDTO.DAL.Interfaces {
    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll();
        Task<T> GetAsync(long id);
        void Update(T item);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void CreateAsync(T item);
        void Delete(T item);
        Task<int> SaveChangesAsync();
    }
}
