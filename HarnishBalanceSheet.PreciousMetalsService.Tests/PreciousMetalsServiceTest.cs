
using HarnishBalanceSheet.PreciousMetalsService;
using Microsoft.Extensions.Configuration;

namespace HarnishBalanceSheet.PreciousMetalsService.Tests
{
    [TestClass]
    public sealed class PreciousMetalsServiceTest
    {
        [TestMethod]
        public async Task GetPreciousMetalsPricesAsyncTest()
        {
            IConfigurationRoot root = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .Build();

            IConfiguration configuration = root as IConfiguration;
            Assert.IsNotNull(configuration);

            PreciousMetalsService preciousMetalsService = new PreciousMetalsService(configuration);

            var result = await preciousMetalsService.GetPreciousMetalsPricesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
            foreach (var element in result)
            {
                Assert.IsGreaterThan(0, element.Price);
            }
        }
    }
}
