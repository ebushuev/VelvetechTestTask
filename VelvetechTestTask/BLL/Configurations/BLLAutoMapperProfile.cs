using AutoMapper;
using BLL.Models;
using DAL.DataTransferObjects;

namespace BLL.Configurations
{
    public class BLLAutoMapperProfile : Profile
    {
        public BLLAutoMapperProfile() 
        {
            CreateMap<ToDoItem, ToDoItemDTO>().ReverseMap();    
        }
    }
}
