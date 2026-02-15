using System;
using System.Collections.Generic;
using HarnishBalanceSheet.Models;

namespace HarnishBalanceSheet.DataAccess
{
    public interface IBalanceSheetRepository
    {
        Task<IEnumerable<BalanceSheetLinkItem>> GetBalanceSheetDatesAsync(int userId, int count);
        Task<IEnumerable<LiabilityChartItem>> GetLiabilitiesAsync(int userId, int count);
        Task<IEnumerable<NetWorthChartModel>> GetNetWorthChartModelsAsync(int userId, int count);
        Task<IEnumerable<AssetCategory>> HasTargetsAsync(int userId);
        Task<int> SetTargetsAsync(int userId, IEnumerable<Target> targets);
        Task<Details> GetDetailsAsync(int userId, int balanceSheetId);
        Task<EditModel> GetEditModelAsync(int userId, int balanceSheetId);
        Task<EditModel> GetLatestAsync(int userId);
        Task<int> CreateBalanceSheetAsync(BalanceSheet balanceSheet);
        Task<int> EditBalanceSheetAsync(BalanceSheet balanceSheet);

    }
}
