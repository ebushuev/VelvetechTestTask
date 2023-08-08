using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.DAL.EF;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Interfaces;

namespace TodoApi.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<TodoItem> TodoRepository { get; private set; }

        private readonly TodoContext _db;

        public UnitOfWork(TodoContext db)
        {
            _db = db;
            TodoRepository = new TodoRepository(_db);
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
