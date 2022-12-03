using System;
using TodoApi.Models;
using TodoApiDTO.DAL;
using TodoApiDTO.DAL.Interfaces;

namespace TodoApiDTO.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TodoContext db;
        private TodoItemRepository todoItemRepository;
        private bool disposed;

        public EFUnitOfWork(TodoContext context)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TodoItem> TodoItems
        {
            get
            {
                if (todoItemRepository == null)
                    todoItemRepository = new TodoItemRepository(db);
                return todoItemRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
