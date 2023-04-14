namespace TodoApi.DAL.Repositories.Contracts 
{
    public interface IRepository<T> where T : class
    {
        public Task<T> Create(T obj);
        public Task<bool> Delete(long id);
        public Task<bool> Update(T item);
        public Task<IEnumerable<T>> GetAll();
        public Task<T?> GetById(long id);
    }
}