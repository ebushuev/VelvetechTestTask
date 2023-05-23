using AutoMapper;
using Velvetech.MyTodoApp.Application.DTOs;
using Velvetech.TodoApp.Domain.Entities;

namespace Velvetech.MyTodoApp.Application.Profiles
{
    public class TodoItemProfile : Profile
    {
        // Source -> Target
        public TodoItemProfile()
        {
            CreateMap<TodoItemEntity, TodoItemReadDto>().ReverseMap();
            CreateMap<TodoItemEntity, TodoItemCreateDto>().ReverseMap();
            CreateMap<TodoItemEntity, TodoItemUpdateDto>().ReverseMap();
        }
    }
}
