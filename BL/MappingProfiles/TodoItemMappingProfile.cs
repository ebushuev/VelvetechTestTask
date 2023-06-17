using AutoMapper;
using Todo.BL.DTOs;
using Todo.DAL.Entities;

namespace Todo.BL.MappingProfiles
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile() {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
            CreateMap<CreateTodoItemDTO, TodoItem>();
        }
    }
}
