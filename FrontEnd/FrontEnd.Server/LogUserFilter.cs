
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace HarnishBalanceSheet.Server
{
    public class LogUserFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                context.HttpContext.Items["CurrentUserId"] = userId;
            }
            else
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    error = "Unauthorized user",
                    timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
