using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Contracts;
using Todo.Data.Entities;

namespace Todo.Domain.Mapper
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItemDTO, TodoItem>().ReverseMap();
        }
    }
}
