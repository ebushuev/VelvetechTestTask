using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.DAL.EF;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Interfaces;

namespace TodoApi.DAL.Repositories
{
    public class TodoRepository : IRepository<TodoItem>
    {
        private readonly TodoContext _db;

        public TodoRepository(TodoContext db)
        {
            _db = db;
        }

        public virtual IQueryable<TodoItem> GetAll()
        {
            return _db.TodoItems.AsQueryable();
        }

        public virtual async Task<TodoItem> GetAsync(int id)
        {
            var entity = await _db.TodoItems.FindAsync(id);

            return entity;
        }

        public virtual void Add(TodoItem entity)
        {
            _db.TodoItems.Add(entity);
        }

        public virtual async Task<bool> UpdateAsync(TodoItem entity)
        {
            var todoItem = await _db.TodoItems.FindAsync(entity.Id);

            if (todoItem != null)
            {
                todoItem.Name = entity.Name;
                todoItem.IsComplete = entity.IsComplete;

                _db.TodoItems.Update(todoItem);

                return true;
            }

            return false;
        }

        public virtual async Task<bool> RemoveAsync(int id)
        {
            var todoItem = await _db.TodoItems.FindAsync(id);

            if (todoItem != null)
            {
                _db.TodoItems.Remove(todoItem);

                return true;
            }

            return false;
        }
    }
}
