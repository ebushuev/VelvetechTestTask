using AutoMapper;
using TodoApiDto.StrongId;
using DbData = TodoApiDto.Repositories.Data;
using ServiceData = TodoApiDto.Services.Data;

namespace TodoApi.Controllers.Configuration.AutoMapper.AutoMapperProfiles
{
    public class TodoItemServiceDataProfile : Profile
    {
        public TodoItemServiceDataProfile()
        {
            CreateMap<ServiceData.TodoItemCreateModel, DbData.TodoItemCreateModel>();

            CreateMap<ServiceData.TodoItemUpdateModel, DbData.TodoItemUpdateModel>()
                .ForMember(todoItem => todoItem.Id, opt => opt.MapFrom(todoItem => todoItem.Id.ObjectId));

            CreateMap<DbData.TodoItem, ServiceData.TodoItem>()
                .ForMember(todoItem => todoItem.Id, opt => opt.MapFrom(todoItem => new TodoId(todoItem.Id)));
        }
    }
}