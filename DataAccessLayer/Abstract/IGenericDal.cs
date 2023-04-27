using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.EntityLayer.Entities.Abstract;

namespace TodoApi.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T: BaseEntity
    {
        public Task<T> CreateAsync(T item);
        public Task<T> GetAsync(long id);
        public Task<List<T>> GetAllAsync();
        public Task UpdateAsync(long id, T item);
        public Task DeleteAsync(long id);
    }
}
