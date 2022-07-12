using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Models;

namespace Todo.DAL.Managers
{
    public class TodoItemsManager : ManagerBase
    {
        public TodoItemsManager(TodoContext context) : base(context)
        {
        }

        #region list
        public DbSet<MTodoItem> GetTodoItems()
        {
            return Context.TodoItems;
        }
        #endregion

        #region CRUD
        public async Task<MTodoItem> GetTodoItem(Guid id)
        {
            return await Context.TodoItems.FindAsync(id);
        }

        public async Task CreateTodoItem(MTodoItem obj)
        {
            Context.TodoItems.Add(obj);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateTodoItem(MTodoItem obj)
        {
            Context.TodoItems.Update(obj);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteTodoItem(MTodoItem obj)
        {
            Context.TodoItems.Remove(obj);
            await Context.SaveChangesAsync();
        }
        #endregion

        private async Task<bool> TodoItemExists(Guid id) => await Context.TodoItems.AnyAsync(e => e.Id == id);
    }
}
