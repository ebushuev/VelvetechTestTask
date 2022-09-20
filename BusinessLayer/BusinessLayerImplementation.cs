using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.DataAccessLayer;
using TodoApiDTO.Extensions;

namespace TodoApiDTO.BusinessLayer
{
    public class BusinessLayerImplementation : IBusinessLayer
    {
        private readonly IDataAccessLayer _dal;

        public BusinessLayerImplementation(IDataAccessLayer DAL)
        {
            _dal = DAL;
        }
        public async Task<OperationEventResult> ExecuteTodoItemFetch(long? id = null)
        {
            if(id == null)
            {
                List<TodoItem> items = await _dal.GetTodoItems();
                return new OperationEventResult
                {
                    Type = OperationEventType.Done,
                    Payload = items.Select(x => x.ToDTO()).ToList(),
                };
            }
            else
            {
                TodoItem targetItem = await _dal.GetTodoItem(id.Value);
                return targetItem == null ? 
                new OperationEventResult
                {
                    Type = OperationEventType.NotFound,
                    Payload = null
                }
                :
                new OperationEventResult
                {
                    Type = OperationEventType.Done,
                    Payload = 
                    new List<TodoItemDTO>
                    {
                        targetItem.ToDTO()
                    }
                };
            }
        }
        public async Task<OperationEventResult> ExecuteTodoItemUpdate(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                new OperationEventResult
                {
                    Type = OperationEventType.BadRequest
                };
            }

            TodoItem targetItem = await _dal.GetTodoItem(id);
            if(targetItem == null)
            {
                return new OperationEventResult
                {
                    Type = OperationEventType.NotFound
                };
            }

            await _dal.UpdateTodoItem(todoItemDTO);

            return new OperationEventResult
            {
                Type = OperationEventType.NoContent
            };
        }
        public async Task<OperationEventResult> ExecuteTodoItemCreate(TodoItemDTO todoItemDTO)
        {
            TodoItem createdItem = await _dal.CreateTodoItem(todoItemDTO);

            return new OperationEventResult
            {
                Type = OperationEventType.Done,
                Payload = new List<TodoItemDTO>
                {
                    createdItem.ToDTO()
                }
            };
        }
        public async Task<OperationEventResult> ExecuteTodoItemDelete(long id)
        {
            TodoItem targetItem = await _dal.GetTodoItem(id);
            if(targetItem == null)
            {
                return new OperationEventResult
                {
                    Type = OperationEventType.NotFound
                };
            }

            await _dal.DeleteTodoItem(targetItem);

            return new OperationEventResult
            {
                Type = OperationEventType.NoContent
            };
        }
    }
}
