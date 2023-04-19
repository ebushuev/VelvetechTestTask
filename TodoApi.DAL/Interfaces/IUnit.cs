using System;
using TodoApi.DAL.Entities;

namespace TodoApi.DAL.Interfaces
{
    public interface IUnit:IDisposable
    {
        IRepository<TodoItem> TodoItems { get; }
        void Save();
    }
}
