using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace TodoApiDTO.Controllers {
    public class ErrorController : Controller {

        private readonly ILogger<ErrorController> logger;

        public ErrorController( ILogger<ErrorController> logger ) {
            this.logger = logger ?? throw new ArgumentNullException ( nameof ( logger ) );
        }

        [Route ( "Error" )]
        [ApiExplorerSettings ( IgnoreApi = true )]
        [AllowAnonymous]
        public void ExceptionHandler() {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature> ();
            if(exceptionDetails != null) {
                logger.LogError ( $"The path: {exceptionDetails.Path}\nException: {exceptionDetails.Error}" );
            }
        }
    }
}
