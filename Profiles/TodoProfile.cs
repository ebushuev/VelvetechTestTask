using AutoMapper;
using TodoApi.Models;
using TodoApiDTO.Dtos;


namespace TodoApiDTO.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
        }
    }
}