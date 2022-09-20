using DAL.DataContext;
using DAL.Entity;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Repository {
	public class TodoRepository : ITodoRepository {
		private readonly TodoContext _context;

		public TodoRepository(TodoContext context) {
			_context = context;
		}
		public async Task<TodoItem> GetAsync(long id) {
			return await _context.TodoItems.Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<TodoItem>> GetAllAsync() {
			return await _context.TodoItems.ToListAsync();
		}

		public async Task SaveAsync(TodoItem entity) {
			await _context.TodoItems.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(TodoItem entity) {
			_context.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(TodoItem entity) {
			_context.Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
