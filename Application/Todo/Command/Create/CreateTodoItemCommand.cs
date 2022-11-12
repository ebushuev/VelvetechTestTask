using Application.Common.Mapping;
using AutoMapper;
using Domain;
using MediatR;
using System;

namespace Application.Todo.Command.Create
{
    public class CreateTodoItemCommand : IRequest<TodoItem>, IMapWith<TodoItem>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTodoItemCommand, TodoItem>();
        }
    }
}
