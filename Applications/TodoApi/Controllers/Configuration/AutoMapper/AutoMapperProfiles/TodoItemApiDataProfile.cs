using AutoMapper;
using ApiData = TodoApiDto.Shared.Api.Data;
using ServiceData = TodoApiDto.Services.Data;

namespace TodoApi.Controllers.Configuration.AutoMapper.AutoMapperProfiles
{
    public class TodoItemApiDataProfile : Profile
    {
        public TodoItemApiDataProfile()
        {
            CreateMap<ApiData.Requests.TodoItemCreateRequest, ServiceData.TodoItemCreateModel>();
            CreateMap<ApiData.Requests.TodoItemUpdateRequest, ServiceData.TodoItemUpdateModel>();
            CreateMap<ServiceData.TodoItem, ApiData.TodoItemViewModel>()
                .ForMember(todoItem => todoItem.Id, opt => opt.MapFrom(todoItem => todoItem.Id.ObjectId));
        }
    }
}