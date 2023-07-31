using FluentValidation;

namespace Todo.Core.Business.TodoItem.Commands.Create
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(x => "Name is required for the Todo item and can't be an empty string");
        }
    }
}
