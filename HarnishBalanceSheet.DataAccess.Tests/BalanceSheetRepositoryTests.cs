using HarnishBalanceSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HarnishBalanceSheet.DataAccess.Tests
{
    [TestClass]
    public sealed class BalanceSheetRepositoryTests
    {
        private BalanceSheetRepository _repository;
        private int _userId = 1;

        [TestInitialize]
        public void CreateBalanceSheetRepository()
        {
            IConfigurationRoot root = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .Build();

            IConfiguration configuration = root as IConfiguration;
            Assert.IsNotNull(configuration);

            var connString = configuration["MySettings:DbConnectionString"];

            Assert.IsNotNull(connString);

            var optionsBuilder = new DbContextOptionsBuilder<BalanceSheetContext>();
            optionsBuilder.UseSqlServer(connString);
            BalanceSheetContext balanceSheetContext = new BalanceSheetContext(optionsBuilder.Options);
            _repository = new BalanceSheetRepository(balanceSheetContext);
        }

        [TestMethod]
        public async Task HasTargetsAsyncTest()
        {
            var assetCategories = await _repository.HasTargetsAsync(_userId);

            Assert.IsNotNull(assetCategories);
        }

        [TestMethod]
        public async Task SetTargetsAsyncTest()
        {
            var targets = new List<Target>()
            {
                new Target()
                {
                    AssetCategoryId = 1,
                    Percentage = 0.2m
                },
                new Target()
                {
                    AssetCategoryId = 2,
                    Percentage = 0.1m
                },
                new Target()
                {
                    AssetCategoryId = 3,
                    Percentage = 0.1m
                },
                new Target()
                {
                    AssetCategoryId = 4,
                    Percentage = 0.2m
                },
                new Target()
                {
                    AssetCategoryId = 5,
                    Percentage = 0.4m
                }
            };

            var result = await _repository.SetTargetsAsync(_userId, targets);

            Assert.AreEqual(targets.Count, result);
        }
    }
}
