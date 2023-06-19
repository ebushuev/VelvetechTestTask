using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.BLL.Exceptions;
using TodoApiDTO.DAL.Repositories;
using TodoApiDTO.Models;

namespace TodoApiDTO.BLL
{
    public class TodoItemManager : ITodoItemManager
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemManager(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<TodoItemDTO>> GetAllTodoItems()
        {
            var items = await _repository.GetAll();
            return items.Select(ItemToDTO).ToList();
        }

        public async Task<TodoItemDTO> GetTodoItemById(long id)
        {
            var item = await _repository.GetById(id);
            if (item == null)
            {
                throw new EntityNotFoundException();
            }

            return ItemToDTO(item);
        }

        public async Task UpdateTodoItem(long id, TodoItemDTO newTodoItem)
        {
            var todoItem = await _repository.GetById(id);
            if (todoItem == null)
            {
                throw new EntityNotFoundException();
            }

            todoItem.Name = newTodoItem.Name;
            todoItem.IsComplete = newTodoItem.IsComplete;

            await _repository.SaveAsync();
        }

        public async Task<TodoItemDTO> Create(TodoItemDTO todoItemDto)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDto.IsComplete,
                Name = todoItemDto.Name
            };
            await _repository.Create(todoItem);
            await _repository.SaveAsync();
            return ItemToDTO(todoItem);
        }

        public async Task DeleteTodoItem(long id)
        {
            var item = await _repository.GetById(id);
            if (item == null)
            {
                throw new EntityNotFoundException();
            }
            _repository.Delete(item);
            await _repository.SaveAsync();
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
