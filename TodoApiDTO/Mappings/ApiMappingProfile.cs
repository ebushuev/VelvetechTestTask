using AutoMapper;
using TodoApiDTO.BLL.DTOs.TodoItems;
using TodoApiDTO.Models.TodoItems;

namespace TodoApiDTO.Mappings
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateTodoItemRequestModel, CreateTodoItemRequestDTO>();
            CreateMap<CreateTodoItemResponseDTO, TodoItemResponseModel>();
            CreateMap<UpdateTodoItemRequestModel, UpdateTodoItemRequestDTO>();
            CreateMap<TodoItemResponseDTO, TodoItemResponseModel>();
        }
    }
}
