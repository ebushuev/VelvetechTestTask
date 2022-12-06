using System;
using TodoApi.Models;

namespace TodoApiDTO.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TodoItem> TodoItems { get; }
        void Save();
    }
}
