using AutoMapper;
using TodoApi.Models;

namespace TodoApi.Services.Configuration
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.IsComplete, opt => opt.MapFrom(o => o.IsComplete));

            CreateMap<TodoItemDTO, TodoItem>()
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.IsComplete, opt => opt.MapFrom(o => o.IsComplete))
                .ForMember(x => x.Secret, opt => opt.Ignore());
        }
    }
}