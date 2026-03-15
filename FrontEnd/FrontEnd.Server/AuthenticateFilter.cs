
using HarnishBalanceSheet.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace HarnishBalanceSheet.Server
{   
    public class AuthenticateFilter : IActionFilter
    {
        private IBalanceSheetBL _balanceSheetBL;

        public AuthenticateFilter(IBalanceSheetBL balanceSheetBL)
        {
            _balanceSheetBL = balanceSheetBL;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items["CurrentUserId"] = 1;

            /*var user = context.HttpContext.User;            

            if (user?.Identity?.IsAuthenticated == true)
            {
                string email = user?.FindFirstValue("preferred_username");

                if (email != null)
                { 
                    var userfromDb = _balanceSheetBL.GetUser(email);

                    if (userfromDb != null) 
                        context.HttpContext.Items["CurrentUserId"] = userfromDb.UserId;
                }
            }

            if (context.HttpContext.Items["CurrentUserId"] == null)
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    error = "Unauthorized user",
                    timestamp = DateTime.UtcNow
                });
            }  */
        }
    }
}
