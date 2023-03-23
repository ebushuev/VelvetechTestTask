using System.Threading.Tasks;
using TodoApiDTO.Core.Models;
using TodoApiDTO.Core.Services;

namespace TodoApiDTO.Api.Validation
{
    public class TodoItemCreateDTOValidator : BaseModelValidator<TodoItemCreateDTO>
    {
        public TodoItemCreateDTOValidator(ITodoService todoService)
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
                if (await todoService.GetNameIsUsedAsync(dto.Name))
                {
                    return $"Name '{dto.Name}' is not unique.";
                }

                return null;
            });
        }
    }
}