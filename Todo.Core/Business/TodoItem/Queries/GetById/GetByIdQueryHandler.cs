using AutoMapper;
using MediatR;
using Todo.Core.Business.TodoItem.Dto;
using Todo.Core.Business.TodoItem.Interfaces;
using Todo.Core.Exceptions;

namespace Todo.Core.Business.TodoItem.Queries
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, TodoItemDto>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<TodoItemDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var todoItem = await _todoRepository.FindByIdAsync(request.Id, cancellationToken);

            if (todoItem == null)
            {
                throw new NotFoundException($"Todo item {request.Id} not found");
            }

            return _mapper.Map<TodoItemDto>(todoItem);
        }
    }
}
