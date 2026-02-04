using System;
using System.Collections.Generic;
using HarnishBalanceSheet.Models;

namespace HarnishBalanceSheet.DataAccess
{
    public interface IBalanceSheetContext
    {
        Task<IEnumerable<BalanceSheet>> GetBalanceSheetDatesAsync(int userId, int count);
        Task<IEnumerable<Liability>> GetLiabilitiesAsync(int userId, int count);
        Task<IEnumerable<BalanceSheet>> GetBalanceSheetsAsync(int userId, int count);
        Task<bool> HasTargetsAsync(int userId);
        Task<bool> SetTargetsAsync(int userId, IEnumerable<Target> targets);
        Task<BalanceSheet> GetDetailsAsync(int userId, int balanceSheetId);
        Task<bool> CreateBalanceSheetAsync(int userId, BalanceSheet balanceSheet);
        Task<bool> EditBalanceSheetAsync(int userId, BalanceSheet balancesheet);

    }
}
