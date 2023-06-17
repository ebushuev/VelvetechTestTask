using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.BL.DTOs;
using Todo.DAL.DbContexts;

namespace Todo.BL.UseCases.TodoItems.Queries.GetTodoItem
{
    public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItemDTO>
    {
        private readonly IMapper _mapper;
        private readonly TodoContext _context;

        public GetTodoItemQueryHandler(IMapper mapper, TodoContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<TodoItemDTO> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.TodoItems
                .Where(x => x.Id == request.Id)
                .ProjectTo<TodoItemDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return item ?? throw new Exception("Не найден TodoItem");
        }
    }
}
