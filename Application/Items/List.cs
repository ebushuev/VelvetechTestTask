

using Application.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Items
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<TodoItemDTO>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<TodoItemDTO>>>
        {
            private readonly TodoContext _context;
            private readonly IMapper _mapper;
            public Handler(TodoContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<PagedList<TodoItemDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.TodoItems
                .ProjectTo<TodoItemDTO>(_mapper.ConfigurationProvider)
                .AsQueryable();

                return Result<PagedList<TodoItemDTO>>.Success(await PagedList<TodoItemDTO>.CreateAsync(query,
                     request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}
