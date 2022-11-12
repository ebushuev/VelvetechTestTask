using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Todo.Command.Delete
{
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly IDbContext _dbContext;
        public DeleteTodoItemCommandHandler(IDbContext dbContext) => _dbContext = dbContext;
        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _dbContext.TodoItems
               .FindAsync(new object[] { request.Id }, cancellationToken);

            if (item == null)
                throw new NotFoundException(HttpStatusCode.NotFound,$"TodoItem {request.Id} not found");

            _dbContext.TodoItems.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
