using AutoMapper;
using Todo.Core.Business.TodoItem.Commands.Update;
using Todo.Core.Business.TodoItem.Dto;

namespace Todo.Core.Business.TodoItem.Mappings
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<Entities.TodoItem, TodoItemDto>();
            CreateMap<TodoItemDto, Entities.TodoItem>();
            CreateMap<UpdateCommand, Entities.TodoItem>();
        }
    }
}
