using AutoMapper;
using System.Threading.Tasks;
using TodoCore.Data.Entities;
using TodoCore.Data.Interfaces;
using TodoCore.DTOs;
using TodoCore.Exceptions;
using TodoCore.Services;

namespace TodoApplication.Services
{
    internal class GetTodoItemService : IGetTodoItemService
    {
        private readonly ITodoItemRepository _todoItemReposytory;
        private readonly IMapper _mapper;

        public GetTodoItemService(ITodoItemRepository todoItemReposytory, IMapper mapper)
        {
            _todoItemReposytory = todoItemReposytory;
            _mapper = mapper;
        }

        public async Task<TodoItemDTO> GetTodoItemAsync(long id)
        {
            try
            {
                var entity = await _todoItemReposytory.GetByIdAsync(id);
                return _mapper.Map<TodoItemDTO>(entity);
            }
            catch(EntityNotFoundException<TodoItem> ex)
            {
                throw ex;
            }
        }
    }
}
