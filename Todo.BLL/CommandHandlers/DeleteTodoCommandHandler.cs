using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Todo.BLL.Queries;
using Todo.Common.Commands;
using Todo.Common.Exceptions;
using Todo.DAL;
using Todo.DAL.Models;

namespace Todo.BLL.CommandHandlers
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
    {
        private readonly IMediator _mediator;
        private readonly TodoContext _context;

        public DeleteTodoCommandHandler(IMediator mediator, TodoContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task Handle(DeleteTodoCommand request, CancellationToken ct)
        {
            var todoItem = await _mediator.Send(new GetTodoEntityQuery(request.Id), ct) ?? throw new NotFoundException();

            _context.Set<TodoItemEntity>().Remove(todoItem);

            try
            {
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                await _mediator.Send(new GetTodoEntityQuery(request.Id));

                if (todoItem == null) throw new NotFoundException();

                throw;
            }
        }
    }
}
