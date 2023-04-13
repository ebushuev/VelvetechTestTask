using AutoMapper;
using TodoApi.BLL.Models;
using TodoApi.DAL.Models;

namespace TodoApi.BLL.Extensions.MappingProfiles;

public class TodoItemMappingProfile : Profile
{
    public TodoItemMappingProfile()
    {
        CreateMap<TodoItemDto, TodoItemEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Secret, opt => opt.Ignore())
            .ReverseMap();
    }
}