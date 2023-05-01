using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.DataAccessLayer;
using TodoApiDTO.BusinessLogicLayer.Interfaces;
using TodoApiDTO.DataAccessLayer.Repositories;

namespace TodoApiDTO.BusinessLogicLayer.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IMapper _mapper;
        private readonly IToDoRepository<TodoItem> _repository;
        public ToDoService(IToDoRepository<TodoItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task DeleteToDoItemByIdAsync(TodoItemDTO modelDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(modelDTO);
            await _repository.DeleteAsync(todoItem);
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync()
        {
            var items = await _repository.GetAllAsync();

            var itemDTOs = _mapper.Map<IEnumerable<TodoItemDTO>>(items);

            return itemDTOs;
        }

        public async Task<long> UpsertToDoItemsAsync(TodoItemDTO modelDTO)
        {
            if (modelDTO.Id == 0)
            {
                var item = _mapper.Map<TodoItem>(modelDTO);
                item.InitCreate();
                var result = await _repository.AddAsync(item);

                return result.Id;   
            }
            else
            {
                var model = await _repository.GetByIdAsync(modelDTO.Id);
                model.InitChange();
                await _repository.UpdateAsync(model);

                return model.Id;
            }
        }

        public async Task<TodoItemDTO> GetToDoItemByIdAsync(long id)
        {
            var toDo = await _repository.GetByIdAsync(id);

            if (toDo != null)
                return null;

            var toDoDto = _mapper.Map<TodoItemDTO>(toDo);

            return toDoDto;
        }
    }
}
