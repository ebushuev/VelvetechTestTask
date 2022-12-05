using AutoMapper;
using ToDo.Application.Models;
using ToDo.WebApi.Models;

namespace ToDo.WebApi.Mapping
{
    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            CreateMap<ToDoDto, TodoItemVM>().ForSourceMember(dto => dto.Secret, opt => opt.DoNotValidate())
                .ReverseMap();
        }
    }
}