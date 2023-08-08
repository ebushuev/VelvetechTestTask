using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.DAL.EF;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Repositories;

namespace TodoApi.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<TodoItem> TodoRepository { get; }

        Task<int> SaveAsync();
    }
}
