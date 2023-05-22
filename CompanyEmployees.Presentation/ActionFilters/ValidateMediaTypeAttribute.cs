using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace CompanyEmployees.Presentation.ActionFilters;

public class ValidateMediaTypeAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var acceptHeaderPresent = context.HttpContext.Request.Headers.ContainsKey("Accept");

        if (!acceptHeaderPresent)
        {
            context.Result = new BadRequestObjectResult("Accept header is missing.");
            return;
        }

        var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

        if (!MediaTypeHeaderValue.TryParse(mediaType, out var outMediaType))
        {
            context.Result = new BadRequestObjectResult("Media type not present or not valid.");
            return;
        }

        context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}