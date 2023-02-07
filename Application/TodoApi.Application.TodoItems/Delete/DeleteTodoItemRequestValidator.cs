using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Delete
{
    public class DeleteTodoItemRequestValidator : AbstractValidator<DeleteTodoItemRequest>
    {
        private readonly IEntityAccessService<TodoItem> _entityAccessService;

        public DeleteTodoItemRequestValidator(IEntityAccessService<TodoItem> entityAccessService)
        {
            _entityAccessService = entityAccessService;
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Id).CustomAsync(ValidateEntityExists);
        }

        //TODO: Should create a custom validator to follow DRY
        private async Task ValidateEntityExists(
            long value,
            ValidationContext<DeleteTodoItemRequest> context,
            CancellationToken cancellationToken)
        {
            var res = await _entityAccessService.Find(value);

            if (res == null)
            {
                throw new EntityNotFoundException();
            }
        }
    }
}