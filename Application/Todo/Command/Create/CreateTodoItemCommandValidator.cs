using FluentValidation;

namespace Application.Todo.Command.Create
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(256);

            RuleFor(x => x.IsComplete)
                .NotNull();
        }
    }
}
