using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.BLL.Queries;
using Todo.Common.Exceptions;
using Todo.DAL;
using Todo.DAL.Models;

namespace Todo.BLL.QueryHandlers
{
    public class GetTodoEntityQueryHandler : IRequestHandler<GetTodoEntityQuery, TodoItemEntity>
    {
        private readonly TodoContext _context;

        public GetTodoEntityQueryHandler(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItemEntity> Handle(GetTodoEntityQuery request, CancellationToken ct)
        {
            var todoItem = await _context
                .Set<TodoItemEntity>()
                .FindAsync(new object[] { request.Id }, ct);

            return todoItem;
        }
    }
}
