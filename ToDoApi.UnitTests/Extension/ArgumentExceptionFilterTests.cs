using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Extension;

namespace TodoApi.UnitTests.Extension
{
    [TestClass]
    public class ArgumentExceptionFilterTests
    {
        [TestMethod]
        public async Task ArgumentException_ShouldReturnBadRequestObjectResult_WhenExceptionIsArgumentExceptionAndAsync()
        {
            var validateFilter = new ArgumentExceptionFilter();
            var context = CreateContext(new ArgumentException());

            //Act
            await validateFilter.OnExceptionAsync(context);

            //Assert
            Assert.IsNotNull(context.Result);
            Assert.IsInstanceOfType(context.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task ArgumentException_ShouldReturnBadRequestObjectResult_WhenExceptionIsArgumentNullExceptionAndAsync()
        {
            var validateFilter = new ArgumentExceptionFilter();
            var context = CreateContext(new ArgumentNullException());

            //Act
            await validateFilter.OnExceptionAsync(context);

            //Assert
            Assert.IsNotNull(context.Result);
            Assert.IsInstanceOfType(context.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void ArgumentException_ShouldReturnBadRequestObjectResult_WhenExceptionIsArgumentException()
        {
            var validateFilter = new ArgumentExceptionFilter();
            var context = CreateContext(new ArgumentException());

            //Act
            validateFilter.OnException(context);

            //Assert
            Assert.IsNotNull(context.Result);
            Assert.IsInstanceOfType(context.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void ArgumentException_ShouldReturnBadRequestObjectResult_WhenExceptionIsArgumentNullExceptionn()
        {
            var validateFilter = new ArgumentExceptionFilter();
            var context = CreateContext(new ArgumentNullException());

            //Act
            validateFilter.OnException(context);

            //Assert
            Assert.IsNotNull(context.Result);
            Assert.IsInstanceOfType(context.Result, typeof(BadRequestObjectResult));
        }

        private ExceptionContext CreateContext(Exception ex)
        {
            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(
                httpContext: httpContext,
                routeData: new RouteData(),
                actionDescriptor: new ActionDescriptor()
            );

            var context = new ExceptionContext(
            actionContext,
            new List<IFilterMetadata>());

            context.Exception = ex;
            return context;
        }
    }
}
