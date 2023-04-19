using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.BL.Interfaces;
using TodoApiDTO.DAL.Data;
using TodoApiDTO.DAL.Models;
using TodoApiDTO.Models;

namespace TodoApiDTO.BL.Service
{
    public class TodoItemService : ITodoItemService
    {
        private readonly TodoContext _context;

        public TodoItemService( TodoContext context )
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync()
        {
            return await _context.TodoItems
                .Select( x => ItemToDTO( x ) )
                .ToListAsync();
        }

        public async Task<TodoItemDTO> GetTodoItemAsync( long id )
        {
            var todoItem = await _context.TodoItems.FindAsync( id );

            if( todoItem == null ) {
                return null;
            }

            return ItemToDTO( todoItem );
        }

        public async Task<TodoItemDTO> CreateTodoItemAsync( TodoItemDTO todoItemDto )
        {
            var todoItem = new TodoItem
            {
                Id = todoItemDto.Id,
                IsComplete = todoItemDto.IsComplete,
                Name = todoItemDto.Name
            };

            _context.TodoItems.Add( todoItem );
            await _context.SaveChangesAsync();

            return ItemToDTO( todoItem );
        }

        public async Task UpdateTodoItemAsync( long id, TodoItemDTO todoItemDto )
        {
            if( id != todoItemDto.Id ) {
                throw new ArgumentException( "Id mismatch" );
            }

            var todoItem = await _context.TodoItems.FindAsync( id );
            if( todoItem == null ) {
                throw new Exception( "Todo item not found" );
            }

            todoItem.Name = todoItemDto.Name;
            todoItem.IsComplete = todoItemDto.IsComplete;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTodoItemAsync( long id )
        {
            var todoItem = await _context.TodoItems.FindAsync( id );

            if( todoItem == null ) {
                throw new Exception( "Todo item not found" );
            }

            _context.TodoItems.Remove( todoItem );
            await _context.SaveChangesAsync();
        }

        private static TodoItemDTO ItemToDTO( TodoItem todoItem ) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
