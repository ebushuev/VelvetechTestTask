using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataLayer.Context;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TodoItemService : ITodoItemService
    {

        private TodoContext _context;

        public TodoItemService(TodoContext context)
        {
            _context = context;
        }

      

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            return await _context.TodoItems.Select(x=>new TodoItemDTO()
            {
                Name = x.Name,  
                Id  =x.Id,
                IsComplete=x.IsComplete   

            }).ToListAsync();
        }

        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItem = _context.TodoItems.Where(x => x.Id == id).Select(x=>new TodoItemDTO()
            {
                Name = x.Name,
                Id = x.Id,
                IsComplete = x.IsComplete

            }).FirstOrDefaultAsync();   


            return await todoItem;

        }

        public bool TodoItemExists(long id)
        {
          return  _context.TodoItems.Any(x => x.Id == id);
        }

        public async void UpdateTodoItem(long id,CreateOrUpdateTodoItemDTO item)
        {
            var _item =_TodoItem(id);
            _item.IsComplete = item.IsComplete;
            _item.Name = item.Name;
             
            _context.Update(_item);
          await  _context.SaveChangesAsync();
        }


        public async void DeleteTodoItem(long id)
        {
            
           _context.Remove(_TodoItem(id));
         await   _context.SaveChangesAsync();
           
        }

        private   TodoItem _TodoItem  (long id)
        {
            return _context.TodoItems.Find(id);
        }

       public async  Task<long> CreateTodoItem(CreateOrUpdateTodoItemDTO item)
        {
            var newitem = new TodoItem()
            {

                Name = item.Name,
                IsComplete = item.IsComplete,
                Secret = "generate secret"

            };
            _context.Add(newitem);
          await  _context.SaveChangesAsync();

            return   newitem.Id;
        }


    }
}
