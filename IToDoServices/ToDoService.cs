using TodoApi.Models;
using TodoApiDTO.Repositories.Entities;
using TodoApiDTO.Repositories.ToDo;

namespace TodoApiDTO.IToDoServices
{
    public class ToDoService : IToDoService
    {
        private TodoContext _todoContext;
        private IToDoRepository _toDoRepository;   
        
        public IToDoRepository _todoRepository
        {
            get
            {
                if (_toDoRepository == null) { _toDoRepository = new ToDoRepository(_todoContext); }
                return _toDoRepository;
            }
        }

        public ToDoService(TodoContext todoContext)
        {
                _todoContext = todoContext; 
        }

        public void Save()
        {
            _todoContext.SaveChanges();
        }
    }
}
