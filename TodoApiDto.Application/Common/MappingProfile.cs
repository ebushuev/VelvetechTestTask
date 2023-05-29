using AutoMapper;
using TodoApiDto.Application.Implementations.Command.CreateToDoItem;
using TodoApiDto.Application.Implementations.Command.UpdateToDoItem;
using TodoApiDto.Domain.Entities;

namespace TodoApiDto.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTodoItemCommand, TodoItem>();
            CreateMap<UpdateToDoItemCommand, TodoItem>();
        }
    }
}
