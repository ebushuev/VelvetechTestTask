
using Application.Features.ToDo.GetToDoItemsById;
using Domain;
using Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ToDo.GetToDoItemsByItems
{
    public class GetTodoItemsByIdQueryHandler : IRequestHandler<GetTodoItemsByIdQuery, TodoItem>
    {
        private readonly ITodoItemRepositoryAsync _repository;

        public GetTodoItemsByIdQueryHandler(ITodoItemRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> Handle(GetTodoItemsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetTodoItemByIdAsync(request.TodoItemId);
        }
    }
}
