using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.BLL.DTO;
using TodoApi.BLL.Interfaces;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Interfaces;

namespace TodoApi.BLL.Services
{
    public class TodoService : ITodoService
    {
        IUnit DB { get; set; }
        public TodoService(IUnit unit)
        {
            this.DB = unit;
        }

        public void Dispose()
        {
            DB.Dispose();
        }

        public TodoItemDTO GetTodoItem(long id)
        {
            var item = DB.TodoItems.Get(id);
            if(item == null)
            {
                return null;
            }
            return ItemToDTO(item);
        }

        public IEnumerable<TodoItemDTO> GetTodoItems()
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<TodoItem, TodoItemDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TodoItem>, List<TodoItemDTO>>(DB.TodoItems.GetAll());
        }

        private TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
        public long CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            TodoItem item = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };
            var itemID = DB.TodoItems.Create(item);
            DB.Save();
            return itemID;
        }
        public void DeleteTodoItem(long id)
        {
            DB.TodoItems.Delete(id);
            DB.Save();
        }

        public void UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var item = DB.TodoItems.Get(id);
            if (item != null)
            {
                item.Name = todoItemDTO.Name;
                item.IsComplete = todoItemDTO.IsComplete;
                DB.TodoItems.Update(item);
                DB.Save();
            }
        }
    }
}
