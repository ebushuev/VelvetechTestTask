using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.DAL.DBContext;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Interfaces;

namespace TodoApi.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TodoContext db;
        private TodoItemRepository todoItemRepository;

        public EFUnitOfWork(DbContextOptions<TodoContext> options)
        {
            db = new TodoContext(options);
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
