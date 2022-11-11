using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.DAL.Entities.Abstractions;

namespace TodoApiDTO.DAL.Repositories.Abstractions
{
    public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(long id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
