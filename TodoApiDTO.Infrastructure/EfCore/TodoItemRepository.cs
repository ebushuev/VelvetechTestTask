using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApiDTO.Domain;

namespace TodoApiDTO.Infrastructure.EfCore
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoApiDTOContext _db;

        public TodoItemRepository(TodoApiDTOContext db)
        {
            _db = db;
        }

        public Task<TodoItem> GetById(long id)
        {
            return _db.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> Create(TodoItem item)
        {
            _db.TodoItems.Add(item);

            await _db.SaveChangesAsync();

            return item.Id;
        }

        public Task Save(TodoItem item)
        {
            // todo some mapping or something else
            return _db.SaveChangesAsync();
        }

        public Task Delete(TodoItem item)
        {
            _db.TodoItems.Remove(item);

            return _db.SaveChangesAsync();
        }
    }
}
