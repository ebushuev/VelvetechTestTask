using Application.Common.Mapping;
using Application.Todo.Command.Create;
using AutoMapper;

namespace TodoApi.Models
{
    public class CreateTodoItemDTO: IMapWith<CreateTodoItemCommand>
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTodoItemDTO, CreateTodoItemCommand>();
        }
    }
}
