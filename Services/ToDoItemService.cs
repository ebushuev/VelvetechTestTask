using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Helpers;
using TodoApiDTO.Models;
using TodoApiDTO.Repository;

namespace TodoApiDTO.Services
{
    public class ToDoItemService
    {
        private ToDoItemsRepository _repo;

        public ToDoItemService(ToDoItemsRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            var items = await _repo.GetAll();
            var dtos = items.Select(c =>
            {
                return Mapper.ToDto(c);
            });
            return dtos;
        }

        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var item = await _repo.GetById(id);
            return Mapper.ToDto(item);
        }

        public async Task<TodoItemDTO> CreateTodoItem(CreateTodoItemRequest model)
        {
            var item = await _repo.Create(Mapper.ToModel(model));
            return Mapper.ToDto(item);
        }

        public async Task UpdateTodoItem(UpdateTodoItemRequest model)
        {
            await _repo.Update(Mapper.ToModel(model));
        }
        public async Task DeleteTodoItem(long id)
        {
            await _repo.Delete(id);
        }
    }
}
