using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.EF;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ToDoRepository : IRepository<TodoItemDTO>
    {
        private TodoContext DB;

        public ToDoRepository(TodoContext context)
        {
            DB = context;
        }

        public async Task<IEnumerable<TodoItemDTO>> ReadAll()
        {
            return await (from todo in DB.TodoItems
                          select todo
                          ).ToListAsync();
        }
        public async Task<TodoItemDTO> Read(long id)
        {
            return await (from todo in DB.TodoItems
                          where todo.Id == id
                          select todo
                ).FirstOrDefaultAsync();

        }
        public void Create(TodoItemDTO Todo)
        {
            DB.TodoItems.Add(Todo);
            return;
        }
        public async void Update(TodoItemDTO Todo)
        {
            var previousTodo = await (from todo in DB.TodoItems
                                      where todo.Id == todo.Id
                                      select todo
                                     ).FirstOrDefaultAsync();

            if (previousTodo != null)
            {
                DB.TodoItems.Remove(previousTodo);
                TodoItemDTO newTodo = new TodoItemDTO()
                {
                    Name = Todo.Name,
                    IsComplete = Todo.IsComplete,
                };

                DB.TodoItems.Add(newTodo);
            }
        }
        public void Delete(long id)
        {
            TodoItemDTO Todo = DB.TodoItems.Find(id);
            if (Todo != null)
                DB.TodoItems.Remove(Todo);
        }
    }
}
