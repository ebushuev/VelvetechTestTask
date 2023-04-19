using System;
using System.Collections.Generic;
using TodoApi.DAL.EF;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Interfaces;

namespace TodoApi.DAL.Repositories
{
    public class EFUnit : IUnit
    {
        private readonly TodoContext db;
        private TodoItemRepository todoItemRepository;

        public EFUnit(TodoContext context)
        {
            this.db = context;
        }

        public IRepository<TodoItem> TodoItems
        {
            get
            {
                if(todoItemRepository == null)
                {
                    todoItemRepository = new TodoItemRepository(db);
                }
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

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
