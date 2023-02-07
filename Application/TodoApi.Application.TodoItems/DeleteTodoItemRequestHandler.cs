using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems
{
    public class DeleteTodoItemRequestHandler : IRequestHandler<DeleteTodoItemRequest>
    {
        private readonly IEntityAccessService<TodoItem> _entityAccessService;
        private readonly IEntityModificationService<TodoItem> _entityModificationService;
        private readonly ICommitter _committer;

        public DeleteTodoItemRequestHandler(
            IEntityAccessService<TodoItem> entityAccessService,
            IEntityModificationService<TodoItem> entityModificationService,
            ICommitter committer)
        {
            _entityAccessService = entityAccessService;
            _entityModificationService = entityModificationService;
            _committer = committer;
        }

        public async Task<Unit> Handle(DeleteTodoItemRequest request, CancellationToken cancellationToken)
        {
            var todoItem = await _entityAccessService.Find(request.Id);

            _entityModificationService.Remove(todoItem);

            await _committer.Commit();

            return Unit.Value;
        }
    }
}