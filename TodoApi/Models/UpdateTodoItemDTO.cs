using Application.Common.Mapping;
using Application.Todo.Command.Update;
using AutoMapper;
using System;

namespace TodoApi.Models
{
    public class UpdateTodoItemDTO : IMapWith<UpdateTodoItemCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTodoItemDTO, UpdateTodoItemCommand>();
        }
    }
}
