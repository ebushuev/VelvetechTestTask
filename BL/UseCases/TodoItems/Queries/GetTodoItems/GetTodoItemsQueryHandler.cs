using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Todo.BL.DTOs;
using Todo.DAL.DbContexts;

namespace Todo.BL.UseCases.TodoItems.Queries.GetTodoItems
{
    public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, TodoItemDTO[]>
    {
        private readonly IMapper _mapper;
        private readonly TodoContext _dbContext;

        public GetTodoItemsQueryHandler(IMapper mapper, TodoContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<TodoItemDTO[]> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.TodoItems
                        .ProjectTo<TodoItemDTO>(_mapper.ConfigurationProvider)
                        .ToArrayAsync(cancellationToken);
        }
    }
}
