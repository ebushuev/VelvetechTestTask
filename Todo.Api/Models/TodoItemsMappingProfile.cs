using AutoMapper;
using Velvetech.Todo.Logic.Models;
using Velvetech.Todo.Api.Models;

namespace TodoApiDTO.Models
{
  public class TodoItemsMappingProfile : Profile
  {
    public TodoItemsMappingProfile()
    {
      CreateMap<TodoItemDTO, TodoItemModel>()
       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
       .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
       .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete))
       .ForMember(dest => dest.Secret, opt => opt.Ignore())
       .ReverseMap();
    }
  }
}
