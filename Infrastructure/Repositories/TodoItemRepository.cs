using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
	{
		private readonly TodoContext _context;
		public TodoItemRepository(TodoContext context) : base(context)
		{
			_context = context;
		}
	}
}