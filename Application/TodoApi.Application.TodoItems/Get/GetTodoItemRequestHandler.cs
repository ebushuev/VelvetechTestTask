using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Get
{
    public class GetTodoItemRequestHandler : IRequestHandler<GetTodoItemRequest, TodoItem>
    {
        private readonly IEntityAccessService<TodoItem> _entityAccessService;

        public GetTodoItemRequestHandler(IEntityAccessService<TodoItem> entityAccessService)
        {
            _entityAccessService = entityAccessService;
        }

        public async Task<TodoItem> Handle(GetTodoItemRequest request, CancellationToken cancellationToken)
        {
            var result = await _entityAccessService.Find(request.Id);

            return result;
        }
    }
}