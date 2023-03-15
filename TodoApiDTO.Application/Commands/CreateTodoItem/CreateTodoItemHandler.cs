using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApiDTO.Domain;

namespace TodoApiDTO.Application.Commands.CreateTodoItem
{
    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemCommand, long>
    {
        private readonly ITodoItemRepository _repository;

        public CreateTodoItemHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<long> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = TodoItem.Create(request.Name, request.IsComplete);

            var id = await _repository.Create(todoItem);

            return id;
        }
    }
}
