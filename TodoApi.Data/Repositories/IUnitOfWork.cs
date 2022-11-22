namespace TodoApiDTO.Data.Repositories
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
