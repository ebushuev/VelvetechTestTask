

using Application.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Items
{
    public class Details
    {
        public class Query : IRequest<Result<TodoItemDTO>>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TodoItemDTO>>
        {
            private readonly TodoContext _context;
            private readonly IMapper _mapper;
            public Handler(TodoContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<TodoItemDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var todoItem = await _context.TodoItems
                .ProjectTo<TodoItemDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<TodoItemDTO>.Success(todoItem);
            }
        }
    }
}
