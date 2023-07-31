using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Core.Business.TodoItem.Dto;
using Todo.Core.Business.TodoItem.Interfaces;

namespace Todo.Core.Business.TodoItem.Queries
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, TodoItemDto[]>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<TodoItemDto[]> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _mapper
                .ProjectTo<TodoItemDto>(_todoRepository.GetQuery())
                .ToArrayAsync(cancellationToken);
        }
    }
}
