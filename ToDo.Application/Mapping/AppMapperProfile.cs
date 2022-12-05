using AutoMapper;
using ToDo.Application.Models;
using ToDo.Domain.Models;

namespace ToDo.Application.Mapping
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<ToDoItem, ToDoDto>().ReverseMap();
        }
    }
}