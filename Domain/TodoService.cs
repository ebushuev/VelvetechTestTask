using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using Todo.Domain;

namespace Domain
{
    public class TodoService : IDataService<TodoItem>
    {
        private IMapper _mapper;

        public TodoService()
        {

        }

        public Task<List<TodoItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TodoItem entity)
        {
            throw new NotImplementedException();
        }
    }


}