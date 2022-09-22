using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApiDTO.BusinessLayer
{
    public class OperationEventResult
    {
        public OperationEventType Type { get; set; }
        public List<TodoItemDTO> Payload { get; set; } = null;
    }
}
