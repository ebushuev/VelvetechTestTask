using AutoMapper;
using TodoApi.Models;
using TodoApiDTO.DTOs;

namespace GeekStore.API.Core.Configurations
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoItemDTO, TodoItem>()
                .ForMember(x => x.Secret, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateUpdateItemTodoDTO, TodoItem>();
        }
    }
}   