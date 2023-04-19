using System;
using System.Collections.Generic;

namespace TodoApi.DAL.Interfaces
{
    public interface IRepository<T>where T: class
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        long Create(T item);
        void Update(T item);
        void Delete(long id);
    }
}
