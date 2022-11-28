using AutoMapper;
using BLL.Models;
using TodoApi.Models;

namespace TodoApi.Configurations
{
    public class WebAPIAutoMapperProfile : Profile
    {
        public WebAPIAutoMapperProfile() 
        {
            CreateMap<ToDoViewModel, ToDoItem>().ReverseMap();
        }
    }
}
