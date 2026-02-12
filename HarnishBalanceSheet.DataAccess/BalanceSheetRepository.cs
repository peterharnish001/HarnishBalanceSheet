using HarnishBalanceSheet.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Data;
using System.Data.Common;
using System.Linq;
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

            var param3 = new SqlParameter
            {
                ParameterName = "@BalanceSheetId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC dbo.CreateBalanceSheet @UserId @AssetPortions @Bullion @Liabilities @BalanceSheetId OUTPUT",
                balanceSheet.UserId, param, param1, param2, param3);

            return (int)(param3.Value ?? 0);
        }        

        public async Task<int> EditBalanceSheetAsync(BalanceSheet balanceSheet)
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

            return await _context.Database.ExecuteSqlRawAsync("EXEC dbo.EditBalanceSheet @UserId @BalanceSheetId @AssetPortions @Bullion @Liabilities",
                balanceSheet.UserId, balanceSheet.BalanceSheetId, param, param1, param2);
        }

        public async Task<IEnumerable<NetWorthChartModel>> GetNetWorthChartModelsAsync(int userId, int count)
        {
            return await _context.NetWorthChartModels
                .FromSqlInterpolated($"EXEC dbo.GetNetWorthChart {userId} {count}")
                .ToListAsync();
        }

        public async Task<IEnumerable<BalanceSheetLinkItem>> GetBalanceSheetDatesAsync(int userId, int count)
        {
            return await _context.BalanceSheetLinks
                .FromSqlInterpolated($"EXEC dbo.GetBalanceSheetDates {userId} {count}")
                .ToListAsync();
        }

        public async Task<Details> GetDetailsAsync(int userId, int balanceSheetId)
        {
            Details details = new Details();

            using (var conn = _context.Database.GetDbConnection())
            {
                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "GetDetails";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "UserId";
                    param1.DbType = DbType.Int32;
                    param1.Value = userId;
                    cmd.Parameters.Add(param1);

                    DbParameter param2 = cmd.CreateParameter();
                    param2.ParameterName = "BalanceSheetId";
                    param2.DbType = DbType.Int32;
                    param2.Value = balanceSheetId;
                    cmd.Parameters.Add(param2);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        List<AssetCategory> categories = new List<AssetCategory>();
                        BalanceSheet balanceSheet = new BalanceSheet();

                        while (await reader.ReadAsync())
                        {
                            categories.Add(new AssetCategory()
                            {
                                Name = reader.GetString("Name")
                            });
                        }

                        details.AssetTypes = categories;

                        if (await reader.NextResultAsync())
                        {
                            List<AssetPortion> portions = new List<AssetPortion>();

                            while (await reader.ReadAsync())
                            {
                                portions.Add(new AssetPortion()
                                {
                                    AssetCategoryName = reader.GetString("AssetCategoryName"),
                                    AssetName = reader.GetString("AssetName"),
                                    Value = reader.GetDecimal("Value")
                                });
                            }

                            balanceSheet.Assets = portions.Select(x => new Asset()
                            {
                                Name = x.AssetName                       
                            }).Distinct().ToList();

                            ((List<Asset>)balanceSheet.Assets).ForEach(x => x.AssetPortions = portions.Where( y => y.AssetName == x.Name).ToList());
                        }

                        details.BalanceSheet = balanceSheet;
                    }
                }
            }

            return details;
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

        public Task<BalanceSheet> GetBalanceSheetAsync(int userId, int balanceSheetId)
        {
            throw new NotImplementedException();
        }

        private DataTable CreateLiabilityDataTable(ICollection<Liability> liabilities)
        {
            var table = new DataTable();
            table.Columns.Add("LiabilityId", typeof(int?));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Value", typeof(decimal));

            foreach (var liability in liabilities)
            {
                table.Rows.Add(liability.LiabilityId, liability.Name, liability.Value);
            }

            return table;
        }

        private DataTable CreateBullionDataTable(ICollection<MetalPosition> bullion)
        {
            var table = new DataTable();
            table.Columns.Add("MetalPositionId", typeof(int?));
            table.Columns.Add("PreciousMetalId", typeof(int));
            table.Columns.Add("NumOunces", typeof(decimal));
            table.Columns.Add("PricePerOunce", typeof(decimal));

            foreach (var position in bullion)
            {
                table.Rows.Add(position.MetalPositionId, position.PreciousMetalId, position.NumOunces, position.PricePerOunce);
            }

            return table;
        }

        private DataTable CreateAssetPortionDataTable(ICollection<Asset> assets)
        {
            var table = new DataTable();
            table.Columns.Add("AssetPortionId", typeof(int?));
            table.Columns.Add("AssetId", typeof(int?));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("IsPercent", typeof(bool));
            table.Columns.Add("AssetCategoryId", typeof(int));
            table.Columns.Add("Value", typeof(decimal));

            var assetPortions = assets.SelectMany(x => x.AssetPortions.Select(y => new
            {
                y.AssetPortionId,
                x.AssetId,
                x.Name, 
                x.IsPercent,
                y.AssetCategoryId,
                y.Value
            }));

            foreach (var assetPortion in assetPortions)
            {
                table.Rows.Add(
                    assetPortion.AssetPortionId,
                    assetPortion.AssetId,
                    assetPortion.Name,
                    assetPortion.IsPercent,
                    assetPortion.AssetCategoryId,
                    assetPortion.Value);
            }

            return table;
        }        
    }
}
