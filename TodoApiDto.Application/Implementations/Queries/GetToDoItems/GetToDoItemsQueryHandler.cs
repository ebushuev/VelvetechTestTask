using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using TodoApiDto.Application.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TodoApiDto.Domain.Entities;

namespace TodoApiDto.Application.Implementations.Queries.GetItems
{
    public class GetToDoItemsQueryHandler: IRequestHandler<GetToDoItemsQuery, IEnumerable<TodoItem>>
    {
        private readonly ITodoApiDtoDbContext _context;
        public GetToDoItemsQueryHandler(IMapper mapper, ITodoApiDtoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Items.ToListAsync();
            return items;
        }
    }
}
