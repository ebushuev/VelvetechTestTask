using Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ToDo.DeleteTodoItem
{
    public class DeleteTodoItemQueryHandler : IRequestHandler<DeleteTodoItemQuery>
    {
        private readonly ITodoItemRepositoryAsync _repository;

        public DeleteTodoItemQueryHandler(ITodoItemRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTodoItemQuery request, CancellationToken cancellationToken)
        {
            await this._repository.DeleteTodoItemAsync(request.TodoItemId);
            await this._repository.SaveAsync();
        }
    }
}
