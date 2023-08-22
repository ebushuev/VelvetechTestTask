using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services 
{
	public interface ITodoService 
	{
		bool TodoItemExists(long id);

		IQueryable<TodoItemDTO> List();

		Task<TodoItem> FindAsync(long id);

		Task RemoveAsync(TodoItem todoItem, CancellationToken cancellationToken);

		Task CreateAsync(TodoItem todoItem, CancellationToken cancellationToken);

		Task SaveChangesAsync(CancellationToken cancellationToken);
	}
}
