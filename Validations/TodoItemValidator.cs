using FluentValidation;
using TodoApiDTO.Models;

namespace TodoApiDTO.Validations;

public class TodoItemCreateValidator : AbstractValidator<TodoItemCreateModel>
{
    public TodoItemCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);
        
        RuleFor(x => x.Secret)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);
    }
}

public class TodoItemUpdateValidator : AbstractValidator<TodoItemUpdateModel>
{
    public TodoItemUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);
    }
}