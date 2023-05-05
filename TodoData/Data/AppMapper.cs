using AutoMapper;
using TodoEntities.DbSet;
using TodoModels.Models;

namespace TodoData.Data
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
        }
    }
}
