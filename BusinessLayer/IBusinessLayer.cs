using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.BusinessLayer
{
    public interface IBusinessLayer
    {
        public Task<OperationEventResult> ExecuteTodoItemDelete(long id);
        public Task<OperationEventResult> ExecuteTodoItemCreate(TodoItemDTO todoItemDTO);
        public Task<OperationEventResult> ExecuteTodoItemUpdate(long id, TodoItemDTO todoItemDTO);
        public Task<OperationEventResult> ExecuteTodoItemFetch(long? id = null);
    }
}
