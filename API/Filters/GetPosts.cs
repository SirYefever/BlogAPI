using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class GetPosts: Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }
}