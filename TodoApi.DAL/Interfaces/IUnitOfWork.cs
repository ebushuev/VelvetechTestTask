
using TodoApi.DAL.Entities;

namespace TodoApi.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TodoItem> TodoItems { get; }
    }
}
