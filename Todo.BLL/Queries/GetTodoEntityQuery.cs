using MediatR;
using Todo.DAL.Models;

namespace Todo.BLL.Queries
{
    public class GetTodoEntityQuery : IRequest<TodoItemEntity>
    {
        public long Id { get; }

        public GetTodoEntityQuery(long id)
        {
            Id = id;
        }
    }
}
