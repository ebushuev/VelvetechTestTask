using Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ToDo.UpdateTodoItem
{
    public class UpdateTodoItemQureyHandler : IRequestHandler<UpdateTodoItemQuery>
    {
        private readonly ITodoItemRepositoryAsync _repository;

        public UpdateTodoItemQureyHandler(ITodoItemRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTodoItemQuery request, CancellationToken cancellationToken)
        {
            var item = await this._repository.GetTodoItemByIdAsync(request.Id);

            item.Name = request.TodoItem.Name;

            item.IsComplete = request.TodoItem.IsComplete;
            
            await this._repository.UpdateTodoItemAsync(item);

            await this._repository.SaveAsync();
        }
    }
}
