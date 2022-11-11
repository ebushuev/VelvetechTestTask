using AutoMapper;
using TodoApiDTO.BLL.DTOs.TodoItems;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.BLL.Mappings
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateMap<CreateTodoItemRequestDTO, TodoItem>();
            CreateMap<TodoItem, CreateTodoItemResponseDTO>();
            CreateMap<UpdateTodoItemRequestDTO, TodoItem>();
            CreateMap<TodoItem, TodoItemResponseDTO>();
        }
    }
}
