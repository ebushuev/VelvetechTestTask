using DataAccessLayer.EF;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private TodoContext DataBase { get; }
        private ToDoRepository TodoRepository;

        public UnitOfWork()
        {
            this.DataBase = new TodoContext();
        }

        public IRepository<TodoItemDTO> Items
        {
            get
            {
                if (TodoRepository == null)
                    TodoRepository = new ToDoRepository(DataBase);
                return TodoRepository;
            }
        }

        public async void SaveAsync()
        {
            await DataBase.SaveChangesAsync();
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
