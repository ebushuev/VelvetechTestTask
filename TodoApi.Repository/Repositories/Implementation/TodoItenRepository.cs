using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiRepository.Context;
using TodoApiRepository.Models;
using TodoApiRepository.Repositories.Contract;

namespace TodoApiRepository.Repositories.Implementation
{
    public class TodoItenRepository : ITodoItemRepository
    {
        private readonly TodoContext _todoContext;

        public TodoItenRepository(TodoContext todoContext) 
        {
            _todoContext = todoContext;
        }

        /// <inheritdoc/>
        public async Task<ICollection<TodoItem>> GetPagedToDoItemsAsync(int pageSize, int pageNumber) 
        {
            return await _todoContext.TodoItems
                .AsNoTracking()
                .Take(pageSize)
                .Skip((pageNumber - 1) * pageSize)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<TodoItem> GetTodoItemByIdAsync(long id) 
        {
            var todoItem = await _todoContext.TodoItems
                .FindAsync(id)
                .ConfigureAwait(false);

            return todoItem;
        }

        /// <inheritdoc/>
        public async Task AddTodoItemAsync(TodoItem itemToAdd) 
        {
            _todoContext.Add(itemToAdd);
            await _todoContext.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task UpdateTododItem(TodoItem itemToUpdate) 
        {
            _todoContext.Update(itemToUpdate);
            await _todoContext.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task DeleteTodoItem(TodoItem itemToDelete) 
        {
            _todoContext.Remove(itemToDelete);
            await _todoContext.SaveChangesAsync();
        }
    }
}
