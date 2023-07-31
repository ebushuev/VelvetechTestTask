namespace Todo.Core.Common
{
    public interface IRepository<T, in TKey> where T : BaseEntity<TKey>
    {
        IQueryable<T> GetQuery();
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void Add(T entity);
        Task<T> FindByIdAsync(TKey id, CancellationToken cancellationToken, bool noTracking = false);
        Task<T[]> GetAllAsync(CancellationToken cancellationToken, bool noTracking = false);
        void Remove(T entity);
    }
}
