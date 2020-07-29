using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tranlator.Exceptions;
using Tranlator.ViewModels.Errors;

namespace Tranlator.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = context.Exception switch
            {
                RecordNotFoundException e => new ApiError($"{e.Message} not found"),
                _ => new ApiError("Unknown error"),
            };
            context.Result = new JsonResult(error);
        }
    }
}