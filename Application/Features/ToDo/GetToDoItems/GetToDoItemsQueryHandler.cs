

using Domain;
using Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ToDo.GetToDoItems
{
    public class GetTodoItemsByIdQueryHandler : IRequestHandler<GetTodoItemsQuery, ICollection<TodoItem>>
    {
        private readonly ITodoItemRepositoryAsync _repository;

        public GetTodoItemsByIdQueryHandler(ITodoItemRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<TodoItem>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetTodoItemsAsync();
        }
    }
}
