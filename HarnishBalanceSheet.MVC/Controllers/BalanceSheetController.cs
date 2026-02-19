using Microsoft.AspNetCore.Mvc;
using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DTO;

namespace HarnishBalanceSheet.MVC.Controllers
{   
    [ApiController]
    [Route("api/balance-sheet/")]
    public class BalanceSheetController : ControllerBase
    {
        private readonly IBalanceSheetBL _balanceSheetBL;
        private int _userId = 1;

        public BalanceSheetController(IBalanceSheetBL balanceSheetBL)
        {
            _balanceSheetBL = balanceSheetBL;
        }

        [HttpGet("details/{balanceSheetId:int}")]
        public async Task<ActionResult<BalanceSheetDto>> Details(int balanceSheetId)
        {
            return new JsonResult(await _balanceSheetBL.GetDetails(_userId, balanceSheetId));
        }

        [HttpGet("edit/{balanceSheetId:int}")]
        public async Task<ActionResult<List<LiabilityChartDto>>> GetDetailsForEditing(int balanceSheetId)
        {
            return new JsonResult(await _balanceSheetBL.GetBalanceSheetForEditing(_userId, balanceSheetId));
        }

        [HttpGet("create")]
        public async Task<ActionResult<BalanceSheetEditDto>> GetDetailsForCreating()
        {
            return new JsonResult(await _balanceSheetBL.GetBalanceSheetForCreating(_userId));
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Create(BalanceSheetSaveDto balanceSheet)
        {
            return new JsonResult(await _balanceSheetBL.CreateBalanceSheet(_userId, balanceSheet));
        }

        [HttpPut("edit/{balanceSheetId:int}")]
        public async Task<ActionResult<int>> Edit([FromQuery] int balanceSheetId, BalanceSheetEditDto balanceSheet)
        {
            return new JsonResult(await _balanceSheetBL.EditBalanceSheet(_userId, balanceSheetId, balanceSheet));
        }
    }
}
