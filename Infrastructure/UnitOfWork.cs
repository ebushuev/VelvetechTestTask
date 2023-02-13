using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using Infrastructure.Database;

namespace Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly TodoContext _todoContext;
		public ITodoItemRepository TodoItemRepository { get; set; }

		public UnitOfWork(TodoContext todoContext, ITodoItemRepository todoItemRepository)
		{
			_todoContext = todoContext;
			TodoItemRepository = todoItemRepository;
		}

		public async Task<int> SaveAsync() => await _todoContext.SaveChangesAsync();

		public void Dispose() => _todoContext.Dispose();
	}
}