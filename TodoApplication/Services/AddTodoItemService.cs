using AutoMapper;
using System.Threading.Tasks;
using TodoCore.Data.Entities;
using TodoCore.Data.Interfaces;
using TodoCore.DTOs;
using TodoCore.Services;

namespace TodoApplication.Services
{
    public class AddTodoItemService : IAddTodoItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddTodoItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task AddTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            var entity = _mapper.Map<TodoItem>(todoItemDTO);
            _unitOfWork.TodoItemReposytory.Add(entity);
            return _unitOfWork.SaveChangesAsync();
        }
    }
}
