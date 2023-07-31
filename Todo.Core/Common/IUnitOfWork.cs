using System.Threading;
using System.Threading.Tasks;

namespace Todo.Core.Common
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken);
        void Commit();
    }
}
