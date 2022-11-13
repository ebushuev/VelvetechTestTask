using FluentValidation;
using TodoApi.Core.Requests;

namespace TodoApi.Server.Validators
{
    public class PagedTodoItemRequestValidator : AbstractValidator<PagedTodoItemRequest>
    {
        public PagedTodoItemRequestValidator() 
        {
            RuleFor(p => p.PageNumber).NotEmpty();
            RuleFor(p => p.PageSize).NotEmpty();
        }
    }
}
