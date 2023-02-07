using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems
{
    public class GetTodoItemsRequestHandler : IRequestHandler<GetTodoItemsRequest, List<TodoItem>>
    {
        private readonly IEntityAccessService<TodoItem> _entityAccessService;

        public GetTodoItemsRequestHandler(IEntityAccessService<TodoItem> entityAccessService)
        {
            _entityAccessService = entityAccessService;
        }

        public async Task<List<TodoItem>> Handle(GetTodoItemsRequest request, CancellationToken cancellationToken)
        {
            var result = await _entityAccessService.GetAll();

            return result;
        }
    }
}