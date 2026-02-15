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
        Task<IEnumerable<AssetTypeDto>> HasTargets(int userId);
        Task<int> SetTargets(int userId, IEnumerable<TargetDto> targets);
        Task<DetailsDto> GetDetails(int userId, int balanceSheetId);
        Task<BalanceSheetEditDto> GetBalanceSheetForEditing(int userId, int balanceSheetId);
        Task<BalanceSheetEditDto> GetBalanceSheetForCreating(int userId);
        Task<int> CreateBalanceSheet(int userId, BalanceSheetEditDto balanceSheet);
        Task<int> EditBalanceSheet(int userId, BalanceSheetEditDto balanceSheet);

    }
}
