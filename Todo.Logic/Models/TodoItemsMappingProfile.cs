using AutoMapper;
using Velvetech.Todo.Repositories.Entities;

namespace Velvetech.Todo.Logic.Models
{
  public class TodoItemsMappingProfile : Profile
  {
    public TodoItemsMappingProfile()
    {
      CreateMap<DbTodoItem, TodoItemModel>()
       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
       .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
       .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete))
       .ForMember(dest => dest.Secret, opt => opt.MapFrom(src => src.Secret))
       .ReverseMap();
    }
  }
}
