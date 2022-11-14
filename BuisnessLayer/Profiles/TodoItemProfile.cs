using AutoMapper;
using TodoApiDTO.BuisnessLayer.Models;
using TodoApiDTO.DataAccessLayer.Models;

namespace TodoApiDTO.BuisnessLayer.Profiles
{

    /// <summary>
    /// Класс описывающий объекты мапинга DTO и Entity TodoItem
    /// </summary>
    public class TodoItemProfile : Profile
    {
        /// <summary>
        /// Конструктор без параметра
        /// </summary>
        public TodoItemProfile()
        {
            //Мапинг между TodoItem и TodoItemDTO
            CreateMap<TodoItem, TodoItemDTO>();
            CreateMap<TodoItemDTO, TodoItem>();
        }
    }
}
