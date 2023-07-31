using FluentValidation;

namespace Todo.Core.Business.TodoItem.Commands.Delete
{
    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(x => "Id is required");
        }
    }
}
