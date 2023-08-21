namespace TodoApi.Repository
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TodoApi.ObjectModel.Contracts.Repositories;

    internal abstract class UnitOfWork : IUnitOfWork
    {
        private readonly ConcurrentDictionary<Type, object> _repositories = new ConcurrentDictionary<Type, object>();
        private readonly DbContext _context;

        protected UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public async Task ExecuteUnderTransactionAsync(Func<Task> operation, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            
            try
            {
                await operation.Invoke();
                await SaveAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(CancellationToken.None);
                throw;
            }
        }

        public Task SaveAsync(CancellationToken cancellationToken)
            => _context.SaveChangesAsync(cancellationToken);
        
        public TRepository Repository<TRepository>()
        {
            if (_repositories.TryGetValue(typeof(TRepository), out var repository)
                && repository is TRepository result)
            {
                return result;
            }

            throw new ArgumentOutOfRangeException(typeof(TRepository).Name);
        }
        
        protected void RegisterRepository(object repository, Type type)
        {
            _repositories[type] = repository;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}