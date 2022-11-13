using Microsoft.EntityFrameworkCore;
using System;
using TodoApi.DAL.Context;
using TodoApi.DAL.Interfaces;
using TodoApi.DAL.Models;

namespace TodoApi.DAL.Repositories
{
    public class UnitOfWorkService : IUnitOfWork
    {
        private TodoContext db;
        private TodoItemService todoItemRepository;

        public UnitOfWorkService(DbContextOptions<TodoContext> options)
        {
            db = new TodoContext(options);
        }
        public IRepository<TodoItem> TodoItems
        {
            get
            {
                if (todoItemRepository == null)
                    todoItemRepository = new TodoItemService(db);
                return todoItemRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}