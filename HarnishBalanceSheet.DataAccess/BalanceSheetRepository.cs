using HarnishBalanceSheet.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Data;
using System.Text;

namespace HarnishBalanceSheet.DataAccess
{
    public class BalanceSheetRepository : IBalanceSheetRepository
    {
        private readonly BalanceSheetContext _context;

        public BalanceSheetRepository(BalanceSheetContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBalanceSheetAsync(BalanceSheet balanceSheet)
        {
            var dt = CreateAssetPortionDataTable(balanceSheet.Assets);
            var dt1 = CreateBullionDataTable(balanceSheet.Bullion);
            var dt2 = CreateLiabilityDataTable(balanceSheet.Liabilities);

            var param = new SqlParameter
            {
                ParameterName = "@AssetPortions",
                SqlDbType = SqlDbType.Structured,
                Value = dt,
                TypeName = "dbo.AssetPortionTableType"
            };

            var param1 = new SqlParameter
            {
                ParameterName = "@Bullion",
                SqlDbType = SqlDbType.Structured,
                Value = dt1,
                TypeName = "dbo.BullionTableType"
            };

            var param2 = new SqlParameter
            {
                ParameterName = "@Liabilities",
                SqlDbType = SqlDbType.Structured,
                Value = dt2,
                TypeName = "dbo.LiabilityTableType"
            };

            return await _context.Database.ExecuteSqlRawAsync("EXEC dbo.CreateBalanceSheet @UserId @AssetPortions @Bullion @Liabilities",
                balanceSheet.UserId, param, param1, param2);
        }        

        public Task<bool> EditBalanceSheetAsync(BalanceSheet balancesheet)
        {
            throw new NotImplementedException();
        }

        public Task<BalanceSheet> GetBalanceSheetAsync(int userId, int balanceSheetId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BalanceSheetLinkItem>> GetBalanceSheetDatesAsync(int userId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BalanceSheet>> GetBalanceSheetsAsync(int userId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<Details> GetDetailsAsync(int userId, int balanceSheetId)
        {
            throw new NotImplementedException();
        }

        public Task<BalanceSheet> GetLatestAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LiabilityChartItem>> GetLiabilitiesAsync(int userId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasTargetsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetTargetsAsync(int userId, IEnumerable<Target> targets)
        {
            throw new NotImplementedException();
        }

        private DataTable CreateLiabilityDataTable(ICollection<Liability> liabilities)
        {
            var table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Value", typeof(decimal));

            foreach (var liability in liabilities)
            {
                table.Rows.Add(liability.Name, liability.Value);
            }

            return table;
        }

        private DataTable CreateBullionDataTable(ICollection<MetalPosition> bullion)
        {
            var table = new DataTable();
            table.Columns.Add("PreciousMetalId", typeof(int));
            table.Columns.Add("NumOunces", typeof(decimal));
            table.Columns.Add("PricePerOunce", typeof(decimal));

            foreach (var position in bullion)
            {
                table.Rows.Add(position.PreciousMetalId, position.NumOunces, position.PricePerOunce);
            }

            return table;
        }

        private DataTable CreateAssetPortionDataTable(ICollection<Asset> assets)
        {
            var table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("IsPercent", typeof(bool));
            table.Columns.Add("AssetCategoryId", typeof(int));
            table.Columns.Add("Value", typeof(decimal));

            var assetPortions = assets.SelectMany(x => x.AssetPortions.Select(y => new
            {
                x.Name, 
                x.IsPercent,
                y.AssetCategoryId,
                y.Value
            }));

            foreach (var assetPortion in assetPortions)
            {
                table.Rows.Add(assetPortion.Name, assetPortion.IsPercent, assetPortion.AssetCategoryId, assetPortion.Value);
            }

            return table;
        }
    }
}
