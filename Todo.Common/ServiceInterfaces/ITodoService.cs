using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Common.Models.DTO;
using Todo.Common.Models.Responses;

namespace Todo.Common.ServiceInterfaces
{
    public interface ITodoService
    {
        Task<ItemDto> CreateItemAsync(string name, bool isComplete);
        Task DeleteItemAsync(Guid id);
        Task<ItemDto> GetItemAsync(Guid id);
        Task<List<ItemDto>> GetItemsAsync();
        Task<ItemResponse> UpdateItemAsync(ItemDto item);
    }
}