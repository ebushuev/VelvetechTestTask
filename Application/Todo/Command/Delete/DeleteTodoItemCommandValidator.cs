using FluentValidation;
using System;

namespace Application.Todo.Command.Delete
{
    public class DeleteTodoItemCommandValidator : AbstractValidator<DeleteTodoItemCommand>
    {
        public DeleteTodoItemCommandValidator()
        {
            RuleFor(x => x.Id)
                 .NotNull()
                 .NotEqual(Guid.Empty);
        }
    }
}
