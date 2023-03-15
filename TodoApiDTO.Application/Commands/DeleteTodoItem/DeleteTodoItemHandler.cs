using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApiDTO.Domain;

namespace TodoApiDTO.Application.Commands.DeleteTodoItem
{
    public class DeleteTodoItemHandler : AsyncRequestHandler<DeleteTodoItemCommand>
    {
        private readonly ITodoItemRepository _repository;

        public DeleteTodoItemHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        protected override async Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _repository.GetById(request.Id);

            if (todoItem == null)
            {
                throw new NotFoundException();
            }

            await _repository.Delete(todoItem);
        }
    }
}
