using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ReadAll();
        Task<T> Read(long id);
        void Create(T item);
        void Update(T item);
        void Delete(long id);
    }
}
