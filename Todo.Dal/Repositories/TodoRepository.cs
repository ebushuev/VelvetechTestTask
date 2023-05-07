using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Common.Models.Domain;
using Todo.Common.RepositoryInterfaces;
using Todo.Dal.Context;

namespace Todo.Dal.Repositories
{
    internal class TodoRepository : ITodoRepository
    {
        private readonly ITodoContextFactory contextFactory;

        public TodoRepository(ITodoContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task CreateItemAsync(Item item)
        {
            var context = contextFactory.CreateContext();

            await context.Item.AddAsync(item);

            await context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(Item item)
        {
            var context = contextFactory.CreateContext();

            context.Item.Remove(item);

            await context.SaveChangesAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var context = contextFactory.CreateContext();

            return await context.Item.FindAsync(id);
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            var context = contextFactory.CreateContext();

            return await context.Item.ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var context = contextFactory.CreateContext();

            context.Item.Update(item);

            await context.SaveChangesAsync();
        }
    }
}
