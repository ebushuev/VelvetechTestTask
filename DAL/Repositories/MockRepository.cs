using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.DAL.Interfaces;

namespace TodoApiDTO.DAL.Repositories {
    public class MockRepository : IRepository<TodoItem> {

        private List<TodoItem> _items;
        private int id = 4;

        public MockRepository() {
            _items = new List<TodoItem> () {
                new TodoItem { Id = 1, IsComplete = true, Name = "Garold" },
                new TodoItem { Id = 2, IsComplete = true, Name = "Yaroslav" },
                new TodoItem { Id = 3, IsComplete = true, Name = "Natalya" },
                new TodoItem { Id = 4, IsComplete = true, Name = "Marina" },
            };
        }

        public void CreateAsync( TodoItem item ) {
            item.Id = ++id;
            _items.Add ( item );
        }

        public void Delete( TodoItem item ) {
            _items.Remove ( item );
        }

        public IEnumerable<TodoItem> Find( Func<TodoItem, bool> predicate ) {
            return _items.Where ( predicate ).ToList ();
        }

        public IEnumerable<TodoItem> GetAll() {
            return _items;
        }

        public Task<TodoItem> GetAsync( long id ) {
            return Task.Run ( () => _items.FirstOrDefault<TodoItem> ( item => item.Id == id ) );
        }

        public async Task<int> SaveChangesAsync() {
            return 0;
        }

        public void Update( TodoItem item ) {
            var index = _items.FindIndex ( old => old.Id == item.Id );
            if(index is 0) {
                throw new Exception ( $"Not found {item.Name}" );
            }
            _items[index] = item;
        }
    }
}
