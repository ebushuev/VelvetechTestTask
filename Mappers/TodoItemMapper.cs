using TodoApi.Models;

namespace TodoApiDTO.Mappers
{
    public static class TodoItemMapper
    {
        public static TodoItem Map(this TodoItemDTO dto)
        {
            return new TodoItem
            {
                Id = dto.Id,
                Name = dto.Name,
                IsComplete= dto.IsComplete
            };
        }

        public static TodoItemDTO Map(this TodoItem db)
        {
            return new TodoItemDTO 
            {
                Id = db.Id,
                Name = db.Name,
                IsComplete= db.IsComplete
            };
        }
    }
}
