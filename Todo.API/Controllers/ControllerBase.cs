using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.DAL;

namespace Todo.API.Controllers
{
    public class ControllerBase<T> : Controller where T : ControllerBase
    {
        private readonly ILogger<T> _logger;
        protected readonly TodoContext _context;

        public ControllerBase(TodoContext context, ILogger<T> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
