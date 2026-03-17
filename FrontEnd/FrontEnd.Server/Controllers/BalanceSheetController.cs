using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HarnishBalanceSheet.Server.Controllers
{   
    [ApiController]
    [Authorize]
    [Route("api/balance-sheet/")]
    public class BalanceSheetController : BaseController
    {
        private readonly IBalanceSheetBL _balanceSheetBL;        

        public BalanceSheetController(IBalanceSheetBL balanceSheetBL)
        {
            _balanceSheetBL = balanceSheetBL;
        }

        [HttpGet("details/{balanceSheetId:int}")]
        public async Task<ActionResult<BalanceSheetDto>> Details(int balanceSheetId)
        {
            return new JsonResult(await _balanceSheetBL.GetDetails(UserId.Value, balanceSheetId));
        }

        [HttpGet("edit/{balanceSheetId:int}")]
        public async Task<ActionResult<List<LiabilityChartDto>>> GetDetailsForEditing(int balanceSheetId)
        {
            return new JsonResult(await _balanceSheetBL.GetBalanceSheetForEditing(UserId.Value, balanceSheetId));
        }

        [HttpGet("create")]
        public async Task<ActionResult<BalanceSheetEditDto>> GetDetailsForCreating()
        {
            return new JsonResult(await _balanceSheetBL.GetBalanceSheetForCreating(UserId.Value));
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Create(BalanceSheetSaveDto balanceSheet)
        {
            return new JsonResult(await _balanceSheetBL.CreateBalanceSheet(UserId.Value, balanceSheet));
        }

        [HttpPut("edit/{balanceSheetId:int}")]
        public async Task<ActionResult<int>> Edit(int balanceSheetId, BalanceSheetSaveDto balanceSheet)
        {
            return new JsonResult(await _balanceSheetBL.EditBalanceSheet(UserId.Value, balanceSheetId, balanceSheet));
        }
    }
}
