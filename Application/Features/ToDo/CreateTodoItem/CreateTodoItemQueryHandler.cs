
using Domain;
using Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ToDo.CreateTodoItem
{
    public class CreateTodoItemQueryHandler : IRequestHandler<CreateTodoItemQuery, TodoItem>
    {
        private readonly ITodoItemRepositoryAsync _repository;

        public CreateTodoItemQueryHandler(ITodoItemRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> Handle(CreateTodoItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _repository.InsertTodoItemAsync(request.TodoItem);

            await _repository.SaveAsync();
            
            return item;
        }
    }
}
