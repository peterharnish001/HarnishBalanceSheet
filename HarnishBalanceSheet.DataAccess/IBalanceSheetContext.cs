using System;
using System.Collections.Generic;
using HarnishBalanceSheet.Models;

namespace HarnishBalanceSheet.DataAccess
{
    public interface IBalanceSheetContext
    {
        Task<IEnumerable<BalanceSheetLinkItem>> GetBalanceSheetDatesAsync(int userId, int count);
        Task<IEnumerable<LiabilityChartItem>> GetLiabilitiesAsync(int userId, int count);
        Task<IEnumerable<BalanceSheet>> GetBalanceSheetsAsync(int userId, int count);
        Task<bool> HasTargetsAsync(int userId);
        Task<bool> SetTargetsAsync(int userId, IEnumerable<Target> targets);
        Task<BalanceSheet> GetDetailsAsync(int userId, int balanceSheetId);
        Task<BalanceSheet> GetLatestAsync(int userId);
        Task<bool> CreateBalanceSheetAsync(BalanceSheet balanceSheet);
        Task<bool> EditBalanceSheetAsync(BalanceSheet balancesheet);

    }
}
