using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Common.Models.Domain;

namespace Todo.Common.RepositoryInterfaces
{
    public interface ITodoRepository
    {
        Task CreateItemAsync(Item item);
        Task<Item> GetItemAsync(Guid id);
        Task<List<Item>> GetItemsAsync();
        Task RemoveItemAsync(Item item);
        Task UpdateItemAsync(Item item);
    }
}