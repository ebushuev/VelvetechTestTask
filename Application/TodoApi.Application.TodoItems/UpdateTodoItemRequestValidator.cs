using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems
{
    public class UpdateTodoItemRequestValidator : AbstractValidator<UpdateTodoItemRequest>
    {
        private readonly IEntityAccessService<TodoItem> _entityAccessService;

        public UpdateTodoItemRequestValidator(IEntityAccessService<TodoItem> entityAccessService)
        {
            _entityAccessService = entityAccessService;
            RuleFor(x => x.TodoItem).NotNull();
            RuleFor(x => x.Id).Equal(x => x.TodoItem.Id);

            RuleFor(x => x.Id).CustomAsync(ValidateEntityExists);
        }

        //TODO: Should create a custom validator to follow DRY
        private async Task ValidateEntityExists(
            long value,
            ValidationContext<UpdateTodoItemRequest> context,
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