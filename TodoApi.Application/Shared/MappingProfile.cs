using TodoApi.Application.Dto;
using TodoApi.Domain.Models;

namespace TodoApi.Application.Shared
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, TodoItemDto>();
            CreateMap<TodoItemDto, TodoItem>();
            CreateMap<CreateTodoItemDto, TodoItem>();
        }
    }
}
