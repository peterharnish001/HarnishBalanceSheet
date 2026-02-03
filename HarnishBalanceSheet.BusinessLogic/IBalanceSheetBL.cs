using System;
using System.Collections.Generic;
using HarnishBalanceSheet.DTO;

namespace HarnishBalanceSheet.BusinessLogic
{
    public interface IBalanceSheetBL
    {
        List<BalanceSheetDto> GetBalanceSheets(int userId, int num);
        List<LiabilityChartDto> GetLiabilityChart(int userId, int num);
        List<NetWorthChartDto> GetNetWorthChart(int userId, int num);
        bool HasTargets(int userId);
        bool SetTargets(int userId, List<TargetDto> targets);
        DetailsDto GetDetails(int userId, int balanceSheetId);
        BalanceSheetDto GetBalanceSheetForEditing (int userId, int? balanceSheetId);
        bool CreateBalanceSheet(int userId, BalanceSheetEditDto balanceSheet);
        bool EditBalanceSheet(int userId, BalanceSheetEditDto balanceSheet);

    }
}
