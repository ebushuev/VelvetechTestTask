using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Dto;
using TodoApi.Domain.BusinessRules;
using TodoApi.Infrastructure.DataAccess;

namespace TodoApi.Application.Queries.GetTodoItem
{
    internal class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItemDto>
    {
        private readonly ITodoDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITodoItemBusinessRules _businessRules;

        public GetTodoItemQueryHandler(ITodoDbContext context, IMapper mapper, ITodoItemBusinessRules businessRules)
        {
            _context = context;
            _mapper = mapper;
            _businessRules = businessRules;
        }


        public async Task<TodoItemDto> Handle(GetTodoItemQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.TodoItems.AsNoTracking();

            var result = await _businessRules
                .GetByToDoItemId(query, request.Id, cancellationToken);

            return _mapper.Map<TodoItemDto>(result);
        }
    }
}