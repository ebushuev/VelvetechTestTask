
using Application.Common;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Items
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public TodoItemDTO TodoItem { get; set; }
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
                var todoItem = new TodoItem
                {
                    IsComplete = request.TodoItem.IsComplete,
                    Name = request.TodoItem.Name
                };

                _context.TodoItems.Add(todoItem);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create todoItem");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
