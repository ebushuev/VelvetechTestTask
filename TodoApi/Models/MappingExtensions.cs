using Business.Models;

namespace TodoApiDTO.Models
{
    public static class MappingExtensions
    {
        public static TodoItemResponse ToResponse(this TodoItemDto source)
        {
            return new TodoItemResponse
            {
                Id = source.Id,
                Name = source.Name,
                IsComplete = source.IsComplete,
            };
        }
        public static TodoItemDto ToDto(this TodoItemRequest source)
        {
            return new TodoItemDto
            {
                Name = source.Name,
                IsComplete = source.IsComplete,
            };
        }
    }
}
