using Microsoft.EntityFrameworkCore;

namespace Todo.Dal.Context
{
    internal class TodoContextFactory : ITodoContextFactory
    {
        private readonly string connectionString;

        public TodoContextFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public TodoContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<TodoContext>().UseSqlServer(connectionString).Options;

            return new TodoContext(options);
        }
    }
}
