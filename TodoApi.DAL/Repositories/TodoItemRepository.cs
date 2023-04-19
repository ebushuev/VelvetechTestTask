using System;
using System.Collections.Generic;
using TodoApi.DAL.Interfaces;
using TodoApi.DAL.Entities;
using TodoApi.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TodoApi.DAL.Repositories
{
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private TodoContext db;
        
        public TodoItemRepository(TodoContext context)
        {
            this.db = context;
        }
        
        public long Create(TodoItem item)
        {
            db.TodoItems.Add(item);
            return item.Id;
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
            return db.TodoItems.Where(predicate).ToList();
        }

        public TodoItem Get(long id)
        {
            return db.TodoItems.Find(id);
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return db.TodoItems;
        }

        public void Update(TodoItem item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
