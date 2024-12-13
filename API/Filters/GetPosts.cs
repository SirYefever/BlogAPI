using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class GetPosts : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var test = 1;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }
}