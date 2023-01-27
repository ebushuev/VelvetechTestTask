using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using Microsoft.Extensions.Logging;

namespace Domain
{
    public class TodoService : ITodoService
    {
        private IMapper _mapper;
        private ITodoRepository _repository;
        private readonly ILogger<TodoService> _logger;


        public TodoService(IMapper mapper, ITodoRepository repository, ILogger<TodoService> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<TodoItemDTO>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<List<TodoItemDTO>>(items);
        }

        public async Task<TodoItemDTO> GetAsync(Guid id)
        {
            var item = await _repository.GetAsync(id);
            IsEntityExist(item, id);//todo
            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await IsEntityExist(id))
            {
                await _repository.DeleteAsync(id);
            }
        }

        public async Task<TodoItemDTO> UpdateAsync(TodoItemDTO entity)
        {
            var item = await _repository.GetAsync(entity.Id);
            var updated = _mapper.Map<TodoItem>(entity);
            if (IsEntityExist(item, entity.Id))
            {
                await _repository.UpdateAsync(updated);
            }
            return _mapper.Map<TodoItemDTO>(updated);
        }

        public async Task<TodoItemDTO> AddAsync(TodoItemDTO entity)
        {
            var item = await _repository.GetAsync(entity.Id);
            var updated = _mapper.Map<TodoItem>(entity);
            if (IsEntityExist(item, entity.Id))
            {
                await _repository.AddAsync(updated);
            }
            return _mapper.Map<TodoItemDTO>(updated);
        }

        public async Task<bool> IsEntityExist(Guid id)
        {
            var item = await _repository.GetAsync(id);
            return IsEntityExist(item, id);
        }

        private bool IsEntityExist(TodoItem item, Guid id)
        {
            var ifItemExists = item == null;
            if (ifItemExists) return true;
            var exception = $"item type{typeof(TodoItem)} with id={id} not found";
            _logger.LogError(exception);
            throw new KeyNotFoundException(exception);
        }
    }


}