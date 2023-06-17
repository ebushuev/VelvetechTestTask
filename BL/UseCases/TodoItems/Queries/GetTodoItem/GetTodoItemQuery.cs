using MediatR;
using Todo.BL.DTOs;

namespace Todo.BL.UseCases.TodoItems.Queries.GetTodoItem
{
    public class GetTodoItemQuery : IRequest<TodoItemDTO>
    {
        public long Id { get; set; }

        public GetTodoItemQuery(in long id)
        {
            Id = id;
        }
    }
}
