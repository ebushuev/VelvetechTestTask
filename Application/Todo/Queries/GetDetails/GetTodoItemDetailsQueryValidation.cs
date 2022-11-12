using Application.Todo.Command.Update;
using FluentValidation;
using System;

namespace Application.Todo.Queries.GetDetails
{
    public class GetTodoItemDetailsQueryValidation : AbstractValidator<GetTodoItemDetailsQuery>
    {
        public GetTodoItemDetailsQueryValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
        }
    }
}
