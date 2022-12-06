using TodoApi.Models;
using TodoApiDTO.DAL.Interfaces;

namespace TodoApiDTO.DAL.Repositories {
    public class MockUnitOfWork : IUnitOfWork {

        private MockRepository mockRepository;

        public MockUnitOfWork() {
        }

        public IRepository<TodoItem> TodoItems { get => new MockRepository (); }

        public void Dispose() {
        }

        public void Save() {
        }
    }
}
