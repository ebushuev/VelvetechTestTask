

using Application.Common;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Items
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public TodoItemDTO TodoItem { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly TodoContext _context;
            private readonly IMapper _mapper;
            public Handler(TodoContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var todoItem = await _context.TodoItems.FindAsync(request.TodoItem.Id);
                if (todoItem == null) return null;

                _mapper.Map(request.TodoItem, todoItem);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to edit todoItem");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
