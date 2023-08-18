using AutoMapper;
using BusinessLogic;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            //to check error logging
         //   throw new DivideByZeroException();


            List<TodoItem> list = null;

            var db = new BL();
            list = Mapper.Map<List<TodoItem>>(await db.GetToDo());

            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            List<TodoItem> foundToDo = new List<TodoItem>();
            var db = new BL();

            foreach (TodoItem todo in Mapper.Map<List<TodoItem>>(await db.GetToDo()))
            {
                if (todo.Id == id)
                {
                    foundToDo.Add(todo);
                    break;
                }
            }

            if (foundToDo == null)
            {
                return NotFound();
            }
            return foundToDo[0];
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItem todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }
            TodoItem todoItem = null;
            var db = new BL();

            var todo = Mapper.Map<List<TodoItem>>(await db.GetToDo());
            foreach (TodoItem model in todo)
            {
                if (model.Id == id)
                {
                    todoItem = model;
                    break;
                }
            }


            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;


            db.UpdateTodo(Mapper.Map<TodoItemBL>(todoItem));

            return NoContent();
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateTodoItem(TodoItem todoItemModel)
        {
            var db = new BL();
            db.AddToDo(Mapper.Map<TodoItemBL>(todoItemModel));
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemModel.Id }, Mapper.Map<TodoItemBL>(todoItemModel));
        }

        [HttpDelete("{id}")]
        public async void DeleteTodoItem(long id)
        {
            TodoItem todoItem = null;
            var db = new BL();

            var todo = Mapper.Map<List<TodoItem>>(await db.GetToDo());
            foreach (TodoItem model in todo)
            {
                if (model.Id == id)
                {
                    todoItem = model;
                    break;
                }
            }


            if (todoItem == null)
                return;

            db.RemoveToDo(id);
        }

        private bool TodoItemExists(long id)
        {
            var db = new BL();

            var todo = Mapper.Map<List<TodoItem>>(db.GetToDo());
            foreach (TodoItem model in todo)
            {
                if (model.Id == id)
                {
                    return true;
                }
            }


            return false;
        }





        //private static TodoItemModel ItemToDTO(TodoItem todoItem) =>
        //    new TodoItemModel
        //    {
        //        Id = todoItem.Id,
        //        Name = todoItem.Name,
        //        IsComplete = todoItem.IsComplete
        //    };
    }
}
