using System;
using System.Threading.Tasks;
using TodoApiDTO.DAL.Repositories.Abstractions;

namespace TodoApiDTO.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItemRepository { get; }
        Task SaveChangesAsync();
    }
}
