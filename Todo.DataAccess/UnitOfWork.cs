using Todo.Core.Common;

namespace Todo.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoContext _dbContext;

        public UnitOfWork(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
