using FluentValidation;
using TodoApiDTO.Models.TodoItems;

namespace TodoApiDTO.ModelValidations.TodoItems
{
    public class CreateTodoItemRequestModelValidator : AbstractValidator<CreateTodoItemRequestModel>
    {
        public CreateTodoItemRequestModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(512);

            RuleFor(p => p.IsComplete)
                .NotEmpty()
                .NotNull();
        }
    }
}
