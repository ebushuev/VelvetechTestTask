using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.BLL.DTO;

namespace TodoApi.BLL.Interfaces
{
    public interface ITodoService
    {
        TodoItemDTO GetTodoItem(long id);
        IEnumerable<TodoItemDTO> GetTodoItems();
        long CreateTodoItem(TodoItemDTO todoItemDTO);
        void DeleteTodoItem(long id);
        void UpdateTodoItem(long id,TodoItemDTO todoItemDTO);
        void Dispose();
    }
}
