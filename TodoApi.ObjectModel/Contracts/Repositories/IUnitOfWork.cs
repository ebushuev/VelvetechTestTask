namespace TodoApi.ObjectModel.Contracts.Repositories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task ExecuteUnderTransactionAsync(Func<Task> operation, CancellationToken cancellationToken);
        
        TRepository Repository<TRepository>();

        Task SaveAsync(CancellationToken cancellationToken);
    }
}