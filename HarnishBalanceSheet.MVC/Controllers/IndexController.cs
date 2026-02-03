using Microsoft.AspNetCore.Mvc;
using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DTO;

namespace HarnishBalanceSheet.MVC.Controllers
{   
    [ApiController]
    [Route("api[controller]")]
    public class IndexController : ControllerBase
    {
        private IBalanceSheetBL _balanceSheetBL;
        private int _userId = 1;

        public IndexController(IBalanceSheetBL balanceSheetBL)
        {
            _balanceSheetBL = balanceSheetBL;
        }

        [HttpGet("[balancesheets/{num:int}]")]
        public ActionResult<List<BalanceSheetDto>> BalanceSheets(int num)
        {
            return _balanceSheetBL.GetBalanceSheets(_userId, num);
        }

        [HttpGet("[liabilities/{num:int}]")]
        public ActionResult<List<LiabilityChartDto>> LiabilitiesChart(int num)
        {
            return _balanceSheetBL.GetLiabilityChart(_userId, num);
        }

        [HttpGet("[networth/{num:int}]")]
        public ActionResult<List<NetWorthChartDto>> NetWorthChart(int num)
        {
            return _balanceSheetBL.GetNetWorthChart(_userId, num);
        }

        [HttpGet("[hastargets]")]
        public ActionResult<bool> HasTargets()
        {
            return _balanceSheetBL.HasTargets(_userId);
        }

        [HttpPost("[targets]")]
        public ActionResult<bool> Targets(List<TargetDto> targets)
        {
            return _balanceSheetBL.SetTargets(_userId, targets);
        }
    }
}
