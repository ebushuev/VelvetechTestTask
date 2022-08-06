using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TodoApi.UnitTests
{
    public class BaseUnitTest
    {
        protected void CheckNotFoundException(IActionResult actionResult)
        {
            var result = actionResult as IStatusCodeActionResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }
    }
}
