using Application.Common.Mapping;
using AutoMapper;
using Domain;
using MediatR;
using System;

namespace Application.Todo.Command.Update
{
    public class UpdateTodoItemCommand : IRequest, IMapWith<TodoItem>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTodoItemCommand, TodoItem>();
        }
    }
}
