using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Dto;
using TodoApi.Infrastructure.DataAccess;

namespace TodoApi.Application.Queries.GetTodoItemList
{
    internal class GetTodoItemListQueryHandler : IRequestHandler<GetTodoItemListQuery, IEnumerable<TodoItemDto>>
    {
        private readonly ITodoDbContext _context;
        private readonly IMapper _mapper;

        public GetTodoItemListQueryHandler(ITodoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<TodoItemDto>> Handle(GetTodoItemListQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.TodoItems.AsNoTracking();

            var result = await query
                .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);

            return result;
        }
    }
}