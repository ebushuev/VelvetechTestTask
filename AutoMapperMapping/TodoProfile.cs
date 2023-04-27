using AutoMapper;
using TodoApi.EntityLayer.Entities;
using TodoApi.Models;


namespace TodoApi.AutoMapperMapping
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
        }
    }
}