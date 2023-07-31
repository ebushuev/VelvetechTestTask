using AutoMapper;
using MediatR;
using Todo.Core.Business.TodoItem.Interfaces;
using Todo.Core.Common;
using Todo.Core.Exceptions;

namespace Todo.Core.Business.TodoItem.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCommandHandler(
            ITodoRepository todoRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _todoRepository.FindByIdAsync(request.Id, cancellationToken);

            if (todoItem == null)
            {
                throw new NotFoundException($"Todo item { request.Id } not found");
            }

            _mapper.Map(request, todoItem);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
