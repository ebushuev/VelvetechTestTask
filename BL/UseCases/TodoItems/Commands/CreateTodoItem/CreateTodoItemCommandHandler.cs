using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Todo.BL.DTOs;
using Todo.DAL.DbContexts;
using Todo.DAL.Entities;

namespace Todo.BL.UseCases.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommandRequest, TodoItemDTO>
    {
        private readonly IMapper _mapper;
        private readonly TodoContext _context;

        public CreateTodoItemCommandHandler(IMapper mapper, TodoContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<TodoItemDTO> Handle(CreateTodoItemCommandRequest request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<TodoItem>(request.TodoItemDto);

            await _context.TodoItems.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TodoItemDTO>(item);
        }
    }
}