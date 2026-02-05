using Microsoft.AspNetCore.Mvc;
using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DTO;

namespace HarnishBalanceSheet.MVC.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class IndexController : ControllerBase
    {
        private IBalanceSheetBL _balanceSheetBL;
        private int _userId = 1;

        public IndexController(IBalanceSheetBL balanceSheetBL)
        {
            _balanceSheetBL = balanceSheetBL;
        }

        [HttpGet("[balancesheets/{count:int}]")]
        public async Task<ActionResult<IEnumerable<BalanceSheetDto>>> BalanceSheets(int count)
        {
            return new JsonResult(await _balanceSheetBL.GetBalanceSheets(_userId, count));
        }

        [HttpGet("[liabilities/{count:int}]")]
        public async Task<ActionResult<List<LiabilityChartDto>>> LiabilitiesChart(int count)
        {
            return new JsonResult(await _balanceSheetBL.GetLiabilityChart(_userId, count));
        }

        [HttpGet("[networth/{count:int}]")]
        public async Task<ActionResult<List<NetWorthChartDto>>> NetWorthChart(int count)
        {
            return new JsonResult(await _balanceSheetBL.GetNetWorthChart(_userId, count));
        }

        [HttpGet("[hastargets]")]
        public async Task<ActionResult<bool>> HasTargets()
        {
            return await _balanceSheetBL.HasTargets(_userId);
        }

        [HttpPost("[targets]")]
        public async Task<ActionResult<bool>> Targets(List<TargetDto> targets)
        {
            return await _balanceSheetBL.SetTargets(_userId, targets);
        }
    }
}
