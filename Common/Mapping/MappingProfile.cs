using AutoMapper;
using TodoApi.DataAccessLayer;

namespace TodoApiDTO.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
        }
    }
}
