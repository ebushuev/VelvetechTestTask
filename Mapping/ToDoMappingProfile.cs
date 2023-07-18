using AutoMapper;
using TodoApi.Models;

namespace TodoApiDTO.Mapping
{
    public class ToDoMappingProfile : Profile
    {
        public ToDoMappingProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>();
        }

    }
}
