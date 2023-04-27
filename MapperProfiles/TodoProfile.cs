using AutoMapper;
using TodoApiDTO.DAL.Entities;
using TodoApiDTO.Models;

namespace TodoApiDTO.MapperProfiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<TodoItemEntity, TodoItemModel>()
            .ForPath(x => x.Id, opt =>
                opt.MapFrom(x => x.Id))
            .ForPath(x => x.Name, opt =>
                opt.MapFrom(x => x.Name))
            .ForPath(x => x.IsComplete, opt =>
                opt.MapFrom(x => x.IsComplete));

        CreateMap<TodoItemUpdateModel, TodoItemEntity>()
            .ForPath(dst => dst.Id, opt =>
                opt.MapFrom(src => src.Id))
            .ForPath(x => x.Name, opt =>
                opt.MapFrom(x => x.Name))
            .ForPath(x => x.IsComplete, opt =>
                opt.Ignore())
            .ForPath(x => x.Secret, opt =>
                opt.Ignore());
        
    }
}