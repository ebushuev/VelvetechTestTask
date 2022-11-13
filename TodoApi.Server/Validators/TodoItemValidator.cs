using FluentValidation;
using TodoApi.Core.Requests;

namespace TodoApi.Server.Validators
{
    public class TodoItemValidator : AbstractValidator<TodoItemArgs>
    {
        public TodoItemValidator() 
        {
            RuleFor(p => p.Name).MaximumLength(600).NotEmpty();
            RuleFor(p => p.Name);
        }
    }
}
