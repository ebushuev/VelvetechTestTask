using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.DAL.DbContexts;

namespace Todo.BL.UseCases.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommandRequest>
    {
        private readonly TodoContext _context;

        public DeleteTodoItemCommandHandler(TodoContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommandRequest request, CancellationToken cancellationToken)
        {
            var todoItem = await _context.TodoItems.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (todoItem == null)
            {
                throw new Exception("TodoItem for delete has not found");
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}