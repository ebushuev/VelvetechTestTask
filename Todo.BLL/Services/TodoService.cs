using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.DAL.Repositories;
using TodoApi.BLL;
using TodoApi.DAL.Models;

namespace Todo.BLL.Services
{
    internal class TodoService : ITodoService
    {
        private readonly ITodoRepository repository;

        public TodoService(ITodoRepository repository)
        {
            this.repository = repository;
        }
        public async Task<long> CreateAsync(TodoItemDTO item)
        {
            var todo = DtoToItem(item);
            //todo.Secret = "Some Secret";
            // TODO: some business logic
            return await repository.CreateAsync(todo);
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAsync()
        {
            var todo = await repository.GetAsync();
            // TODO: some business logic
            return todo.Select(x => ItemToDTO(x)).ToList();
        }

        public async Task<TodoItemDTO> GetAsync(long id)
        {
            var todo = await repository.GetAsync(id);
            // TODO: some business logic
            return ItemToDTO(todo);
        }

        public async Task UpdateAsync(long id, TodoItemDTO item)
        {
            var todo = DtoToItem(item);
            // TODO: some business logic
            await repository.UpdateAsync(id, todo);
        }

        /// <summary>
        /// Здесь для примера реализовал маппинг так, в идеале нужна библиотека типа Mapster или AutoMapper
        /// </summary>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
           new TodoItemDTO
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
               IsComplete = todoItem.IsComplete
           };
        /// <summary>
        /// Здесь для примера реализовал маппинг так, в идеале нужна библиотека типа Mapster или AutoMapper
        /// </summary>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        private static TodoItem DtoToItem(TodoItemDTO todoItemDto) =>
           new TodoItem
           {
               Id = todoItemDto.Id,
               Name = todoItemDto.Name,
               IsComplete = todoItemDto.IsComplete
           };
    }
}
