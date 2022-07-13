using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApi.Data.Interfaces
{
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class 
    {
        public Task<IReadOnlyCollection<TEntity>> GetAsync();
        public Task<TEntity> GetAsync(TKey id);
        public void Update(TEntity item);
        public void Create(TEntity item);
        public void Delete(TEntity item);
        public Task SaveAsync();
    }
}

