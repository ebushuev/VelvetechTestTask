using System;
using TodoApi.DAL.Models;

namespace TodoApi.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TodoItem> TodoItems { get; }
    }
}
