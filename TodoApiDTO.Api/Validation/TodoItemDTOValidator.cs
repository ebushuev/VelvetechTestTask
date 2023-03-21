using System.Threading.Tasks;
using TodoApiDTO.Core.Models;
using TodoApiDTO.Core.Services;
using TodoApiDTO.EF.Services;

namespace TodoApiDTO.Api.Validation
{
    public class TodoItemDTOValidator : BaseModelValidator<TodoItemDTO>
    {
        public TodoItemDTOValidator(ITodoService todoService)
        {
            AddRule(dto =>
            {
                var message = string.IsNullOrEmpty(dto.Name)
                    ? "Name is required."
                    : null;

                return Task.FromResult(message);
            });

            AddRule(async dto =>
            {
                if (await todoService.GetNameIsUsedExceptOneAsync(dto.Id, dto.Name))
                {
                    return $"Name '{dto.Name}' is not unique.";
                }

                return null;
            });
        }
    }
}