using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.BusinessLayer.Abstract;
using TodoApi.DataAccessLayer.Abstract;
using TodoApi.EntityLayer.Entities;
using TodoApi.Models;

namespace TodoApi.BusinessLayer.Concrete
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly ITodoItemDal _todoDal;
        private readonly IMapper _mapper;

        public TodoItemsService(ITodoItemDal todoDal, IMapper mapper)
        {
            _todoDal = todoDal;
            _mapper = mapper;
        }

        public async Task<TodoItemDTO> CreateTodoAsync(TodoItemDTO item)
        {
            var itemToBeCreated = DTOToEntity(item);
            var createdItem = await _todoDal.CreateAsync(itemToBeCreated);
            var createdItemDTO = EntityToDTO(createdItem);
            return createdItemDTO;
        }

        public async Task DeleteTodoAsync(long id)
        {
             await _todoDal.DeleteAsync(id);
        }

        public async Task<TodoItemDTO> GetTodoAsync(long id)
        {
            var item = await _todoDal.GetAsync(id);
            var itemDto = EntityToDTO(item);
            return itemDto;
        }

        public async Task<List<TodoItemDTO>> GetTodoList()
        {
            var todoList = await _todoDal.GetAllAsync();
            var todoDTOList = _mapper.Map<List<TodoItemDTO>>(todoList);
            return todoDTOList;
        }

        public Task UpdateTodoAsync(long id, TodoItemDTO item)
        {
            var updatedItem = DTOToEntity(item);
            return _todoDal.UpdateAsync(id, updatedItem);

        }
        private TodoItemDTO EntityToDTO(TodoItem todoItem)
        {
            return _mapper.Map<TodoItemDTO>(todoItem);
        }
        private TodoItem DTOToEntity(TodoItemDTO todoItemDTO)
        {
            return _mapper.Map<TodoItem>(todoItemDTO);
        }
    }
}
