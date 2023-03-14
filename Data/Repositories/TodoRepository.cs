using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Models;
using TodoApiDTO.Models;

namespace TodoApiDTO.Data
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> Create(TodoItemDTO input)
        {
            var createdTodoItem = await _context.TodoItems.AddAsync(new TodoItem { IsComplete = input.IsComplete, Name = input.Name });
            await _context.SaveChangesAsync();

            return createdTodoItem?.Entity;
        }

        public async Task<TodoItemActionResult> Delete(long itemToDeleteId)
        {
            var itemToDelete = await _context.TodoItems.FindAsync(itemToDeleteId);

            if (itemToDelete == null)
            {
                return TodoItemActionResult.NotFound;
            }

            _context.TodoItems.Remove(itemToDelete);
            await _context.SaveChangesAsync();

            return TodoItemActionResult.Success;
        }

        public async Task<TodoItem> GetById(long itemId)
        {
            return await _context.TodoItems.FindAsync(itemId);
        }

        public DbSet<TodoItem> GetList()
        {
            return _context.TodoItems;
        }

        public async Task<TodoItemActionResult> Update(TodoItemDTO input)
        {
            var todoItemToUpdate = await _context.TodoItems.FindAsync(input.Id);
            if (todoItemToUpdate == null)
            {
                return TodoItemActionResult.NotFound;
            }

            todoItemToUpdate.Name = input.Name;
            todoItemToUpdate.IsComplete = input.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex) when (!_context.TodoItems.Any(e => e.Id == input.Id))
            {
                throw ex;
            }

            return TodoItemActionResult.Success;
        }
    }
}
