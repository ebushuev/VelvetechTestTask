using AutoMapper;
using Todo.Dtos;
using Todo.DAL.Entities;

namespace Todo;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();

        CreateMap<TodoItemForCreationDto, TodoItem>();

        CreateMap<TodoItemForUpdateDto, TodoItem>().ReverseMap();
    }
}
