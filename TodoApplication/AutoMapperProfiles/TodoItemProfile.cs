using AutoMapper;
using TodoCore.Data.Entities;
using TodoCore.DTOs;

namespace TodoApplication.AutoMapperProfiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>();
            CreateMap<TodoItemDTO, TodoItem>();
        }

    }
}
