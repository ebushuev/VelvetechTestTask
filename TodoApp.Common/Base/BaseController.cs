using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Common.Logging;

namespace TodoApp.Common.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private IAppLogger<T> _logger;
        protected IAppLogger<T> Logger
        {
            get
            {
                return _logger;
            }
        }
    }
}
