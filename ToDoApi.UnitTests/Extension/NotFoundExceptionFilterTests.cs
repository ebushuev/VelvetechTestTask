using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Services.Exceptions;
using TodoApiDTO.Filters;

namespace TodoApi.UnitTests.Extension
{
    [TestClass]
    public class NotFoundExceptionFilterTests
    {
        [TestMethod]
        public async Task NotFoundExceptionFilter_ShouldReturnNotFoundRequestObjectResult_WhenNotFoundExceptionAndAsync()
        {
            var validateFilter = new NotFoundExceptionFilter();
            var context = CreateContext(new NotFoundException("Test"));

            //Act
            await validateFilter.OnExceptionAsync(context);

            //Assert
            Assert.IsNotNull(context.Result);
            Assert.IsInstanceOfType(context.Result, typeof(NotFoundObjectResult));
        }


        [TestMethod]
        public void NotFoundExceptionFilter_ShouldReturnNotFoundObjectResult_WhenNotFoundException()
        {
            var validateFilter = new NotFoundExceptionFilter();
            var context = CreateContext(new NotFoundException("Test"));

            //Act
            validateFilter.OnException(context);

            //Assert
            Assert.IsNotNull(context.Result);
            Assert.IsInstanceOfType(context.Result, typeof(NotFoundObjectResult));
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
