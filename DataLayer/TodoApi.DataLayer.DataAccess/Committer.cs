using System.Threading.Tasks;
using TodoApi.Infrastructure.DbContext;

namespace TodoApi.DataLayer.DataAccess
{
    public class Committer : ICommitter
    {
        private readonly TodoContext _context;

        public Committer(TodoContext context)
        {
            _context = context;
        }

        public Task Commit()
        {
            return _context.SaveChangesAsync();
        }
    }
}