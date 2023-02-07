using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems
{
    public class UpdateTodoItemRequestHandler : IRequestHandler<UpdateTodoItemRequest>
    {
        private readonly IEntityAccessService<TodoItem> _entityAccessService;
        private readonly IEntityModificationService<TodoItem> _entityModificationService;
        private readonly ICommitter _committer;

        public UpdateTodoItemRequestHandler(
            IEntityAccessService<TodoItem> entityAccessService,
            IEntityModificationService<TodoItem> entityModificationService,
            ICommitter committer)
        {
            _entityAccessService = entityAccessService;
            _entityModificationService = entityModificationService;
            _committer = committer;
        }

        public async Task<Unit> Handle(UpdateTodoItemRequest request, CancellationToken cancellationToken)
        {
            var todoItem = await _entityAccessService.Find(request.Id);
            
            todoItem.Name = request.TodoItem.Name;
            todoItem.IsComplete = request.TodoItem.IsComplete;
            
            _entityModificationService.Update(todoItem);

            await _committer.Commit();

            return Unit.Value;
        }
    }
}