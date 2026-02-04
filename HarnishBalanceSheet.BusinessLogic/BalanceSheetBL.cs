using HarnishBalanceSheet.DataAccess;
using HarnishBalanceSheet.DTO;

namespace HarnishBalanceSheet.BusinessLogic
{
    public class BalanceSheetBL : IBalanceSheetBL
    {
        private IBalanceSheetContext _balanceSheetContext;
        public BalanceSheetBL(IBalanceSheetContext context)
        { 
            _balanceSheetContext = context;
        }

        public async Task<bool> CreateBalanceSheet(int userId, BalanceSheetEditDto balanceSheet)
        {
            return await _balanceSheetContext.CreateBalanceSheetAsync(userId, new Models.BalanceSheet());
        }

        public Task<bool> EditBalanceSheet(int userId, BalanceSheetEditDto balanceSheet)
        {
            throw new NotImplementedException();
        }

        public Task<BalanceSheetDto> GetBalanceSheetForEditing(int userId, int balanceSheetId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BalanceSheetDto>> GetBalanceSheets(int userId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<DetailsDto> GetDetails(int userId, int balanceSheetId)
        {
            throw new NotImplementedException();
        }

        public Task<List<LiabilityChartDto>> GetLiabilityChart(int userId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<List<NetWorthChartDto>> GetNetWorthChart(int userId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasTargets(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetTargets(int userId, List<TargetDto> targets)
        {
            throw new NotImplementedException();
        }
    }
}
