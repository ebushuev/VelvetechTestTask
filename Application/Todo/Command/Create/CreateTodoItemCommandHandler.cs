using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Todo.Command.Create
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItem>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateTodoItemCommandHandler(IDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<TodoItem> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<TodoItem>(request);

            await _dbContext.TodoItems.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
