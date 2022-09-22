using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.DataAccessLayer
{
    public interface IDataAccessLayer
    {
        public Task<List<TodoItem>> GetTodoItems();
        public Task<TodoItem> GetTodoItem(long id);
        public Task UpdateTodoItem(TodoItemDTO todoItemDTO);
        public Task<TodoItem> CreateTodoItem(TodoItemDTO todoItemDTO);
        public Task DeleteTodoItem(TodoItem todoItem);
    }
}
