using System.Data;
using System.Threading.Tasks;

namespace TodoCore.Data.Interfaces
{
    public interface IUnitOfWork
    {
        public ITodoItemReposytory TodoItemReposytory { get; }
        public Task SaveChangesAsync();
        public IDbTransaction StartTransation();
    }
}
