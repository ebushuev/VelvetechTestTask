using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Todo.Queries.GetList
{
    public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, TodoDetailsListVm>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTodoListQueryHandler(IDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<TodoDetailsListVm> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
        {
            var entityQuery = await _dbContext.TodoItems
                .AsNoTracking()
                .ProjectTo<TodoDetailsVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new TodoDetailsListVm { TodoDetails = entityQuery };
        }
    }
}
