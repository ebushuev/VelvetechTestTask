using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems
{
    public class CreateTodoItemRequestHandler : IRequestHandler<CreateTodoItemRequest, TodoItem>
    {
        private readonly IEntityModificationService<TodoItem> _entityModificationService;
        private readonly ICommitter _committer;

        public CreateTodoItemRequestHandler(
            IEntityModificationService<TodoItem> entityModificationService,
            ICommitter committer)
        {
            _entityModificationService = entityModificationService;
            _committer = committer;
        }

        public async Task<TodoItem> Handle(CreateTodoItemRequest request, CancellationToken cancellationToken)
        {
            var todoItem = await _entityModificationService.Create(request.TodoItem);

            await _committer.Commit();

            return todoItem;
        }
    }
}