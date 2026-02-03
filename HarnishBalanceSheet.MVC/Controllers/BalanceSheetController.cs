using Microsoft.AspNetCore.Mvc;
using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DTO;

namespace HarnishBalanceSheet.MVC.Controllers
{   
    [ApiController]
    [Route("api[controller]")]
    public class BalanceSheetController : ControllerBase
    {
        private IBalanceSheetBL _balanceSheetBL;
        private int _userId = 1;

        public BalanceSheetController(IBalanceSheetBL balanceSheetBL)
        {
            _balanceSheetBL = balanceSheetBL;
        }

        [HttpGet("[details/{balanceSheetId:int}]")]
        public ActionResult<BalanceSheetDto> Details(int balanceSheetId)
        {
            return new JsonResult(_balanceSheetBL.GetDetails(_userId, balanceSheetId));
        }

        [HttpGet("[edit/{balanceSheetId?:int}]")]
        public ActionResult<List<LiabilityChartDto>> GetDetailsForEditing(int? balanceSheetId)
        {
            return new JsonResult(_balanceSheetBL.GetBalanceSheetForEditing(_userId, balanceSheetId));
        }

        [HttpPost("[create]")]
        public ActionResult<List<NetWorthChartDto>> Create(BalanceSheetEditDto balanceSheet)
        {
            return new JsonResult(_balanceSheetBL.CreateBalanceSheet(_userId, balanceSheet));
        }

        [HttpPut("[edit]")]
        public ActionResult<bool> Edit(BalanceSheetEditDto balanceSheet)
        {
            return new JsonResult(_balanceSheetBL.EditBalanceSheet(_userId, balanceSheet));
        }
    }
}
