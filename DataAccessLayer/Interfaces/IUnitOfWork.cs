using DataAccessLayer.Entities;
using System;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TodoItemDTO> Items { get; }
        void SaveAsync();
    }
}
