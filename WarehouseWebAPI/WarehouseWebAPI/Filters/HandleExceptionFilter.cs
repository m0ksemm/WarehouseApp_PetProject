using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WarehouseWebAPI.Filters
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        public HandleExceptionFilter()
        {

        }

        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult() { Content = context.Exception.Message, StatusCode = 500};
        }
    }
}
