using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TodoApiDTO.Models;

namespace TodoApiDTO.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var responseModel = ApiResponse<object>.Success(objectResult.Value);
                responseModel.StatusCode = context.HttpContext.Response.StatusCode;
                objectResult.Value = responseModel;           
            }
        }
    }
}
