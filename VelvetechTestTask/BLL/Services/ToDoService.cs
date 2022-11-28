using AutoMapper;
using BLL.Infrastructure;
using BLL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using FunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;
using DAL.DataTransferObjects;

namespace BLL.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;

        private readonly IMapper _mapper;

        public ToDoService(IToDoRepository toDoRepository, IMapper mapper)
        {
            _toDoRepository= toDoRepository;
            _mapper= mapper;    
        }

        public async Task<ToDoItem> CreateAsync(ToDoItem entity)
        {
            Validations.ValidateInput(entity, nameof(entity));

            var toDoItem = await _mapper.Map<ToDoItemDTO>(entity)
                                        .FeedTo(_toDoRepository.CreateAsync)
                                        .FeedToAsync(_mapper.Map<ToDoItem>);

            return toDoItem;
        }

        public async Task DeleteByIdAsync(int id) 
            => await _toDoRepository.DeleteByIdAsync(id);

        public async Task<ToDoItem> GetAsync(int id)
            => await _toDoRepository.GetAsync(id)
                                    .FeedToAsync(_mapper.Map<ToDoItem>);

        public async Task<List<ToDoItem>> GetAllAsync() 
            => await _toDoRepository.GetAllAsync()
                                    .FeedToAsync(_mapper.Map<List<ToDoItem>>);

        public async Task<ToDoItem> UpdateAsync(ToDoItem entity)
        {
            Validations.ValidateInput(entity, nameof(entity));

            return await _mapper.Map<ToDoItemDTO>(entity)
                                .FeedTo(_toDoRepository.UpdateAsync)
                                .FeedToAsync(_mapper.Map<ToDoItem>);
        }
    }
}
