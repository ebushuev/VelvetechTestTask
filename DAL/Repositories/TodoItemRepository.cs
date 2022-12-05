using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.DAL.Interfaces;

namespace TodoApiDTO.DAL.Repositories {
    public class TodoItemRepository : IRepository<TodoItem> {

        private TodoContext db;

        public TodoItemRepository( TodoContext context ) {
            db = context ?? throw new ArgumentNullException ( nameof ( context ) );
        }

        public IEnumerable<TodoItem> GetAll() {
            return db.TodoItems.AsNoTracking ();
        }

        public async Task<TodoItem> GetAsync( long id ) {
            return await db.TodoItems.FindAsync ( id );
        }

        public void Update( TodoItem item ) {
            db.Entry ( item ).State = EntityState.Modified;
        }

        public async Task<int> SaveChangesAsync() {
            return await db.SaveChangesAsync ();
        }

        public void CreateAsync( TodoItem item ) {
            db.Entry ( item ).State = EntityState.Added;
        }

        public void Delete( TodoItem item ) {
            db.Entry ( item ).State = EntityState.Deleted;
        }

        public IEnumerable<TodoItem> Find( Func<TodoItem, bool> predicate ) {
            return db.TodoItems.Where ( predicate ).ToList ();
        }
    }
}
