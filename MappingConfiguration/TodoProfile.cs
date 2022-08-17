using AutoMapper;
using Todo.Domain.Models;
using TodoApiDTO.Models;

namespace TodoApiDTO.MappingConfiguration
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoItemModel, TodoItemDTO>();
        }
    }
}
