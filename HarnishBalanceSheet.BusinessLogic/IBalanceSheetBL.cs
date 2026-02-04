using System;
using System.Collections.Generic;
using HarnishBalanceSheet.DTO;

namespace HarnishBalanceSheet.BusinessLogic
{
    public interface IBalanceSheetBL
    {
        Task<List<BalanceSheetDto>> GetBalanceSheets(int userId, int count);
        Task<List<LiabilityChartDto>> GetLiabilityChart(int userId, int count);
        Task<List<NetWorthChartDto>> GetNetWorthChart(int userId, int count);
        Task<bool> HasTargets(int userId);
        Task<bool> SetTargets(int userId, List<TargetDto> targets);
        Task<DetailsDto> GetDetails(int userId, int balanceSheetId);
        Task<BalanceSheetDto> GetBalanceSheetForEditing (int userId, int balanceSheetId);
        Task<bool> CreateBalanceSheet(int userId, BalanceSheetEditDto balanceSheet);
        Task<bool> EditBalanceSheet(int userId, BalanceSheetEditDto balanceSheet);

    }
}
