using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Common.Dto;
using Todo.Common.Queries;
using Todo.DAL;

namespace Todo.BLL.QueryHandlers
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, ICollection<TodoItemDto>>
    {
        private readonly TodoContext _context;

        public GetAllTodosQueryHandler(TodoContext context)
        {
            _context = context;
        }

        public async Task<ICollection<TodoItemDto>> Handle(GetAllTodosQuery request, CancellationToken ct)
        {
            return await _context.TodoItems
                .Select(item => item.Adapt<TodoItemDto>())
                .ToListAsync(ct);
        }
    }
}
