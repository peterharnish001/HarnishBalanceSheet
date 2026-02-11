using System;
using System.Collections.Generic;
using HarnishBalanceSheet.DTO;

namespace HarnishBalanceSheet.BusinessLogic
{
    public interface IBalanceSheetBL
    {
        Task<IEnumerable<BalanceSheetDto>> GetBalanceSheets(int userId, int count);
        Task<IEnumerable<LiabilityChartDto>> GetLiabilityChart(int userId, int count);
        Task<IEnumerable<NetWorthChartDto>> GetNetWorthChart(int userId, int count);
        Task<bool> HasTargets(int userId);
        Task<bool> SetTargets(int userId, IEnumerable<TargetDto> targets);
        Task<DetailsDto> GetDetails(int userId, int balanceSheetId);
        Task<BalanceSheetEditDto> GetBalanceSheetForEditing(int userId, int balanceSheetId);
        Task<BalanceSheetEditDto> GetBalanceSheetForCreating(int userId);
        Task<int> CreateBalanceSheet(int userId, BalanceSheetEditDto balanceSheet);
        Task<bool> EditBalanceSheet(int userId, BalanceSheetEditDto balanceSheet);

    }
}
