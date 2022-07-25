using Application.Common;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Items
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly TodoContext _context;
            public Handler(TodoContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var todoItem = await _context.TodoItems.FindAsync(request.Id);
                _context.TodoItems.Remove(todoItem);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to delete the todoItem");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
