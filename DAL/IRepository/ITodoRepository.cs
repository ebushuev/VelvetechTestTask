using DAL.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.IRepository {
	public interface ITodoRepository {
		Task<TodoItem> GetAsync(long id);
		Task<IEnumerable<TodoItem>> GetAllAsync();
		Task SaveAsync(TodoItem entity);
		Task UpdateAsync(TodoItem entity);
		Task DeleteAsync(TodoItem entity);
	}
}
