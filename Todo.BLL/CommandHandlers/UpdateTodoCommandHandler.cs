using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Todo.BLL.Queries;
using Todo.Common.Commands;
using Todo.Common.Exceptions;
using Todo.DAL;

namespace Todo.BLL.CommandHandlers
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
    {
        private readonly IMediator _mediator;
        private readonly TodoContext _context;

        public UpdateTodoCommandHandler(IMediator mediator, TodoContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task Handle(UpdateTodoCommand request, CancellationToken ct)
        {
            var todoItem = await _mediator.Send(new GetTodoEntityQuery(request.Item.Id)) ?? throw new NotFoundException();

            request.Item.Adapt(todoItem);

            try
            {
                await _context.SaveChangesAsync(ct);
            }
            catch(DbUpdateConcurrencyException)
            {
                await _mediator.Send(new GetTodoEntityQuery(request.Item.Id));

                if (todoItem == null) throw new NotFoundException();

                throw;
            }
        }
    }
}
