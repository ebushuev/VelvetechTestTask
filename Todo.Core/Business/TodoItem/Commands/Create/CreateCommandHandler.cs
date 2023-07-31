using AutoMapper;
using MediatR;
using Todo.Core.Business.TodoItem.Dto;
using Todo.Core.Business.TodoItem.Interfaces;
using Todo.Core.Common;

namespace Todo.Core.Business.TodoItem.Commands.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, TodoItemDto>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommandHandler(
            ITodoRepository todoRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TodoItemDto> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var todoItem = new Entities.TodoItem
            {
                Name = request.Name,
                IsComplete = false
            };

            _todoRepository.Add(todoItem);
            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<TodoItemDto>(todoItem);
        }
    }
}
