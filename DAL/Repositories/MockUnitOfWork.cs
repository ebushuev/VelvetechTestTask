using TodoApi.Models;
using TodoApiDTO.DAL.Interfaces;

namespace TodoApiDTO.DAL.Repositories {
    public class MockUnitOfWork : IUnitOfWork {

        private MockRepository mockRepository;

        public MockUnitOfWork() {
        }

        public IRepository<TodoItem> TodoItems {
            get {
                if(mockRepository == null)
                    mockRepository = new MockRepository ();
                return mockRepository;
            }
        }

        public void Dispose() {
        }

        public void Save() {
        }
    }
}
