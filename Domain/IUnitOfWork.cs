using System;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Domain
{
	public interface IUnitOfWork : IDisposable
	{
		public ITodoItemRepository TodoItemRepository { get; set; }
		Task<int> SaveAsync();
	}
}
