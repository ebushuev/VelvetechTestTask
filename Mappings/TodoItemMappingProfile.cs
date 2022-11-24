using AutoMapper;
using TodoApi.Domain.Models;
using TodoApi.Models;

namespace TodoApiDTO.Mappings
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<TodoItemDTO, TodoItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Secret, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
