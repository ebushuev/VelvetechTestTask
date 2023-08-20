using AutoMapper;
using Todo.Core.Models;
using Todo.Core.Models.TodoItem;
using Todo.Infrastructure.Entities;

namespace Todo.Core.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoItem, TodoItemDTO>();
        CreateMap<TodoItemDTOCreate, TodoItem>();
        CreateMap<TodoItemDTOUpdate, TodoItem>();
    }
}