using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services 
{
	public sealed class TodoService : ITodoService
	{
		private readonly TodoContext _context;

		public TodoService(TodoContext context) 
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id); 

		public IQueryable<TodoItemDTO> List() 
		{
			return _context.TodoItems
				.Select(x => TodoItemDTO.From(x));	
		}

		public async Task<TodoItem> FindAsync(long id) 
		{
			return await _context.TodoItems.FindAsync(id);
		}

		public async Task RemoveAsync(TodoItem todoItem, CancellationToken cancellationToken) 
		{
			_context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task CreateAsync(TodoItem todoItem, CancellationToken cancellationToken)
		{
			_context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken) 
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
