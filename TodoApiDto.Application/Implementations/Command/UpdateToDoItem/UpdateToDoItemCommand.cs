using MediatR;
using Newtonsoft.Json;
using System;

namespace TodoApiDto.Application.Implementations.Command.UpdateToDoItem
{
    public class UpdateToDoItemCommand: IRequest<Unit>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isComplete")]
        public bool IsComplete { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}
