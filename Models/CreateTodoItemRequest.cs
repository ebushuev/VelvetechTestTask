using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.Models
{
    public class CreateTodoItemRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isComplete")]
        public bool IsComplete { get; set; }
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}
