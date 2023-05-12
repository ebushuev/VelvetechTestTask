using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.BLL.Queries;
using Todo.Common.Dto;
using Todo.Common.Exceptions;
using Todo.Common.Queries;

namespace Todo.BLL.QueryHandlers
{
    public class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, TodoItemDto>
    {
        private readonly IMediator _mediator;

        public GetTodoQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TodoItemDto> Handle(GetTodoQuery request, CancellationToken cancellationToken)
        {
            var todoItem = await _mediator.Send(new GetTodoEntityQuery(request.Id));

            if (todoItem == null) throw new NotFoundException();

            return todoItem.Adapt<TodoItemDto>();
        }
    }
}
