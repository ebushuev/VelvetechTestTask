using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Common.Models.Domain;
using Todo.Common.Models.DTO;
using Todo.Common.Models.Responses;
using Todo.Common.RepositoryInterfaces;
using Todo.Common.ServiceInterfaces;

namespace Todo.Bll.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository todoRepository;
        private readonly IMapper mapper;

        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            this.todoRepository = todoRepository;
            this.mapper = mapper;
        }

        public async Task<ItemDto> GetItemAsync(Guid id)
        {
            return mapper.Map<ItemDto>(await todoRepository.GetItemAsync(id));
        }

        public async Task<List<ItemDto>> GetItemsAsync()
        {
            return mapper.Map<List<ItemDto>>(await todoRepository.GetItemsAsync());
        }

        public async Task<ItemDto> CreateItemAsync(string name, bool isComplete)
        {
            var item = new Item
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                IsComplete = isComplete,
                Name = name
            };

            await todoRepository.CreateItemAsync(item);

            return mapper.Map<ItemDto>(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = await todoRepository.GetItemAsync(id);

            if (item != null)
            {
                await todoRepository.RemoveItemAsync(item);
            }
        }

        public async Task<ItemResponse> UpdateItemAsync(ItemDto item)
        {
            var stored = await todoRepository.GetItemAsync(item.Id);
            if (stored is null)
            {
                return new ItemResponse
                {
                    Message = "Entity not found",
                    Item = item
                };
            }

            stored.IsComplete = item.IsComplete;
            stored.Name = item.Name;

            try
            {
                await todoRepository.UpdateItemAsync(stored);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ItemResponse
                {
                    Message = "Entity locked by other user, check new state and try later",
                    Item = item
                };
            }

            return new ItemResponse
            {
                Message = "Entity updated successfully",
                Item = mapper.Map<ItemDto>(stored)
            };
        }
    }
}
