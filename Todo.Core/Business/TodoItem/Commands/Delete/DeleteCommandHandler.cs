using MediatR;
using Todo.Core.Business.TodoItem.Interfaces;
using Todo.Core.Common;
using Todo.Core.Exceptions;

namespace Todo.Core.Business.TodoItem.Commands.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommandHandler(
            ITodoRepository todoRepository,
            IUnitOfWork unitOfWork)
        {
            _todoRepository = todoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _todoRepository.FindByIdAsync(request.Id, cancellationToken);

            if (todoItem == null)
            {
                throw new NotFoundException($"Todo item { request.Id } not found");
            }

            _todoRepository.Remove(todoItem);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
