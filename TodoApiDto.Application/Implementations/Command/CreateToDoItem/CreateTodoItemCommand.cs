using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using TodoApiDto.Application.Common;
using TodoApiDto.Domain.Entities;

namespace TodoApiDto.Application.Implementations.Command.CreateToDoItem
{
    public class CreateTodoItemCommand: IRequest<Guid>
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isComplete")]
        public bool IsComplete { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}
