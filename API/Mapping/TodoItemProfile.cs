using AutoMapper;
using DAL.DTOs;
using DAL.Entity;

namespace TodoApi.Mapping {
	public class TodoItemProfile : Profile {
		public TodoItemProfile() {
			CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
			CreateMap<TodoItem, TodoItemShortDTO>().ReverseMap();
		}
	}
}
