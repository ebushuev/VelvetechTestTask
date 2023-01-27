using AutoMapper;
using DataAccess;

namespace Domain
{
    public class DomainMapperProfile : Profile
    {
        public DomainMapperProfile()
        {
            CreateMap<TodoItemDTO, TodoItem>().ReverseMap();
        }
    }
}
