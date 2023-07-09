using AutoMapper;
using Todo.DAL.Models;
using Todo.Domain.DTOs;

namespace Todo.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
        }
    }
}