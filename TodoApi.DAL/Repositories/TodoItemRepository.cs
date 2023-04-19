using System;
using System.Collections.Generic;
using TodoApi.DAL.Interfaces;
using TodoApi.DAL.Entities;
using TodoApi.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.DAL.Repositories
{
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private TodoContext db;
        
        public TodoItemRepository(TodoContext context)
        {
            this.db = context;
        }
        
        public void Create(TodoItem item)
        {
            db.TodoItems.Add(item);
        }

        public void Delete(long id)
        {
            TodoItem item = db.TodoItems.Find(id);
            if (item != null)
            {
                db.TodoItems.Remove(item);
            }
        }

        public IEnumerable<TodoItem> Find(Func<TodoItem, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public TodoItem Get(long id)
        {
            return db.TodoItems.Find(id);
        }

        public IEnumerable<TodoItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TodoItem item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
