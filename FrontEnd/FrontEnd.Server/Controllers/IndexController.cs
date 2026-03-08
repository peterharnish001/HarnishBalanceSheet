using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HarnishBalanceSheet.Server.Controllers
{   
    [ApiController]
    [Route("api/index/")]
    [Authorize]
    public class IndexController : ControllerBase
    {
        private readonly IBalanceSheetBL _balanceSheetBL;
        
        private int GetUserId()
        {
            return (int)HttpContext.Items["CurrentUserId"];
        }

        public IndexController(IBalanceSheetBL balanceSheetBL)
        {
            _balanceSheetBL = balanceSheetBL;
        }

        [HttpGet("balancesheets")]
        public async Task<ActionResult<IEnumerable<BalanceSheetDto>>> BalanceSheets([FromQuery]int count)
        {
            return new JsonResult(await _balanceSheetBL.GetBalanceSheets(GetUserId(), count));
        }

        [HttpGet("liabilities")]
        public async Task<ActionResult<List<LiabilityChartDto>>> LiabilitiesChart([FromQuery] int count)
        {
            return new JsonResult(await _balanceSheetBL.GetLiabilityChart(GetUserId(), count));
        }

        [HttpGet("networth")]
        public async Task<ActionResult<List<NetWorthChartDto>>> NetWorthChart([FromQuery] int count)
        {
            return new JsonResult(await _balanceSheetBL.GetNetWorthChart(GetUserId(), count));
        }

        [HttpGet("has-targets")]
        public async Task<ActionResult<IEnumerable<AssetTypeDto>>> HasTargets()
        {
            return new JsonResult(await _balanceSheetBL.HasTargets(GetUserId()));
        }

        [HttpPost("set-targets")]
        public async Task<ActionResult<int>> SetTargets(List<SetTargetDto> targets)
        {
            return await _balanceSheetBL.SetTargets(GetUserId(), targets);
        }
    }
}
