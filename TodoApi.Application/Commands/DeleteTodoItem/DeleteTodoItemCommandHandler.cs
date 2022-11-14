using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.BusinessRules;
using TodoApi.Infrastructure.DataAccess;

namespace TodoApi.Application.Commands.DeleteTodoItem
{
    internal class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, bool>
    {
        private readonly ITodoDbContext _context;
        private readonly ITodoItemBusinessRules _businessRules;

        public DeleteTodoItemCommandHandler(ITodoDbContext context, ITodoItemBusinessRules businessRules)
        {
            _context = context;
            _businessRules = businessRules;
        }


        public async Task<bool> Handle(DeleteTodoItemCommand request,
            CancellationToken cancellationToken)
        {
            var query = _context.TodoItems.AsQueryable();

            var item = await _businessRules.GetByToDoItemId(query, request.Id, cancellationToken);

            if (item == null)
                return false;

            _context.TodoItems.Remove(item);

            await _context.SaveChangesAsync(cancellationToken);

            if (await _businessRules.TodoItemExists(query, request.Id, cancellationToken))
                throw new DbUpdateConcurrencyException("Deletion failed");

            return true;
        }
    }
}