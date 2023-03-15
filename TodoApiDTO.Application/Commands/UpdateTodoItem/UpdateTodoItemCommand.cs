using MediatR;
using System.Text.Json.Serialization;

namespace TodoApiDTO.Application.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommand : TodoItemCommand, IRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
    }
}
