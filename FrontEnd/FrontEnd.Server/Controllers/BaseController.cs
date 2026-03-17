using Microsoft.AspNetCore.Mvc;

namespace HarnishBalanceSheet.Server.Controllers
{
    public class BaseController : Controller
    {
        protected int? UserId
        {
            get
            {
                var claim = User.Claims.FirstOrDefault(x => x.Type == "UserId");

                return (claim != null) ? Int32.Parse(claim.Value) : null;
            }
        }
    }
}
