namespace TodoApiDTO.Components.TodoList.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TodoApiDTO.Components.TodoList.DbContexts;
    using TodoApiDTO.Components.TodoList.Interfaces;
    using TodoApiDTO.Components.TodoList.Models;

    /// <summary>
    /// Реализация интерфейса 'Репозиторий' для сущности TO-DO.
    /// </summary>
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _dbContext;

        public TodoRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _dbContext.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> Get(long id)
        {
            return await _dbContext.TodoItems.FindAsync(id);
        }

        public async Task Create(TodoItem item)
        {
            await _dbContext.TodoItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TodoItem item)
        {
            var itemToUpdate = await Get(item.Id);

            itemToUpdate.Name = item.Name;
            itemToUpdate.IsComplete = item.IsComplete;
            itemToUpdate.Secret = item.Secret;

            _dbContext.TodoItems.Update(itemToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var itemToDelete = await Get(id);

            if (itemToDelete == null)
            {
                return;
            }

            _dbContext.TodoItems.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(long id)
        {
            var allItems = await GetAll();

            return allItems
                .Select(x => x.Id)
                .Contains(id);
        }
    }
}