using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.Interfaces;
using Todo.Domain.Models;
using Todo.Infrastructure.DB;

namespace Todo.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<long> Create(TodoItemModel model)
        {
            var item = new TodoItem()
            {
                IsComplete = model.IsComplete,
                Name = model.Name

            };
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return item.Id;
        }

        public async Task Delete(long id)
        {
            var item = _context.TodoItem.FirstOrDefault(m => m.Id == id);

            if (item != null)
            {
                _context.TodoItem.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TodoItemModel> Get(long id)
        {
            var item = await _context.TodoItem.FindAsync(id);

            if (item != null)
            {
                return new TodoItemModel(item.Id, item.Name, item.IsComplete);
            }

            return null;
        }

        public IEnumerable<TodoItemModel> GetAll()
        {
            var items = _context.TodoItem.Select(m=> new TodoItemModel(m.Id, m.Name, m.IsComplete)).ToList();

            return items;
        }

        public async Task Update(TodoItemModel model)
        {
            var item = _context.TodoItem.FirstOrDefault(m => m.Id == model.Id);

            if (item != null)
            {
                item.IsComplete = model.IsComplete;
                item.Name = model.Name;

                await _context.SaveChangesAsync();
            }
        }
    }
}
