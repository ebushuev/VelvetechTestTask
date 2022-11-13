using Application.Common.Mapping;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Todo.Queries
{
    public class TodoDetailsVm : IMapWith<TodoItem>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoDetailsVm>();
        }
    }
}
