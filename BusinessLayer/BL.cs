using System;
using DataAccessLayer;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using BusinessLogic.Entities;
using AutoMapper;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BL : IDisposable
    {
        private UnitOfWork DB { get; }

        public BL()
        {
            DB = new UnitOfWork();
        }

        public void AddToDo(TodoItemBL element)
        {
            DB.Items.Create(Mapper.Map<TodoItemDTO>(element));
            DB.SaveAsync();
        }

        public void RemoveToDo(long id)
        {
            DB.Items.Delete(id);
            DB.SaveAsync();
        }

        public async void UpdateTodo(TodoItemBL element)
        {
            TodoItemDTO toUpdate = await DB.Items.Read(element.Id);

            if (toUpdate != null)
            {
                toUpdate = Mapper.Map<TodoItemDTO>(element);
                DB.Items.Update(toUpdate);
                DB.SaveAsync();
            }
        }

        public async Task<IEnumerable<TodoItemBL>> GetToDo()
        {
            List<TodoItemBL> result = new List<TodoItemBL>();

            foreach (var item in await DB.Items.ReadAll())
                result.Add(Mapper.Map<TodoItemBL>(item));

            return result;
        }

        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
