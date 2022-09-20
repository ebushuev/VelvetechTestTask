using TodoApi.Models;

namespace TodoApiDTO.Extensions
{
    public static class TodoItemExtension
    {
        public static TodoItemDTO ToDTO(this TodoItem item)
        {
            return new TodoItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }
    }
}
