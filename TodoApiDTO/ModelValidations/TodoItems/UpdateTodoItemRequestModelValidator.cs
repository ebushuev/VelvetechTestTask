using FluentValidation;
using TodoApiDTO.Models.TodoItems;

namespace TodoApiDTO.ModelValidations.TodoItems
{
    public class UpdateTodoItemRequestModelValidator : AbstractValidator<UpdateTodoItemRequestModel>
    {
        public UpdateTodoItemRequestModelValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);

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
