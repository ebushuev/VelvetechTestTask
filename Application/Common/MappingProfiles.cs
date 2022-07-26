
using Application.Items;
using Domain;

namespace Application.Common
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<TodoItem, TodoItemDTO>();
        }
    }
}
