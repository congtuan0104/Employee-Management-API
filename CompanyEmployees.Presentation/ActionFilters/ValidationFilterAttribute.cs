using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CompanyEmployees.Presentation.ActionFilters;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) // before action executes
    {
        // get action's name
        var action = context.RouteData.Values["action"];
        // get controller's name
        var controller = context.RouteData.Values["controller"];

        // use the ActionArguments dictionary to extract the DTO parameter
        // that we send to the POST and PUT actions
        var param = context.ActionArguments
            .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

        if (param is null)
        {
            context.Result = new BadRequestObjectResult(
                $"Object for action {action} is null. Controller: {controller} ");
            return;
        }

        if (!context.ModelState.IsValid)
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // after action executes
    }
}