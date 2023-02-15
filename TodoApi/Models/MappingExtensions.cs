using Business.Models;

namespace TodoApiDTO.Models
{
    public static class MappingExtensions
    {
        public static TodoItemResponse ToResponse(this TodoItemDTO source)
        {
            return new TodoItemResponse
            {
                Id = source.Id,
                Name = source.Name,
                IsComplete = source.IsComplete,
            };
        }
        public static TodoItemDTO ToDto(this TodoItemRequest source)
        {
            return new TodoItemDTO
            {
                Name = source.Name,
                IsComplete = source.IsComplete,
            };
        }
    }
}
