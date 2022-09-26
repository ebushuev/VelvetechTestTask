using AutoMapper;
using TodoApi.BLL.Dto;
using TodoApi.DAL.Entity;

namespace TodoApi.BLL.AutomapperProfile {
    public class TidoItemProfile : Profile{
        public TidoItemProfile() {
            CreateMap<TodoItemDTO, TodoItem>().ReverseMap();
        }
    }
}