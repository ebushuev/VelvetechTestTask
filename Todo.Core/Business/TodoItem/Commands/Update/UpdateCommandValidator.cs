using FluentValidation;

namespace Todo.Core.Business.TodoItem.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(x => "Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage(x => "Name is required for the Todo item and can't be an empty string");
        }
    }
}
