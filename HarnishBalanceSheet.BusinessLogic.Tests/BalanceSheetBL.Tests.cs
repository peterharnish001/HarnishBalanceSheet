using AutoMapper;
using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DataAccess;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
using HarnishBalanceSheet.PreciousMetalsService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace HarnishBalanceSheet.BusinessLogic.Tests
{
    [TestClass]
    public sealed class BalanceSheetBLTests
    {
        private BalanceSheetBL _balanceSheetBL;
        private Mock<IBalanceSheetContext> _context;
        private IMapper _mapper;
        private Mock<IPreciousMetalsService> _preciousMetalsService;
        private ILoggerFactory _loggerFactory;
        private int _userId = 1;
        private int _count = 1;

        [TestInitialize]
        public void CreateBusinessLogic()
        {
            _context = new Mock<IBalanceSheetContext>();
            _loggerFactory = new NullLoggerFactory();
            _mapper = CreateMapper();
            _preciousMetalsService = new Mock<IPreciousMetalsService>();            
            _balanceSheetBL = new BalanceSheetBL(_context.Object, _mapper, _preciousMetalsService.Object);
        }        

        [TestMethod]
        public async Task CreateBalanceSheetTest()
        {
            var expected = true;

            _context.Setup(x => x.CreateBalanceSheetAsync(It.IsAny<BalanceSheet>()))
                .Returns(Task.FromResult(expected));

            var result = await _balanceSheetBL.CreateBalanceSheet(_userId, new BalanceSheetEditDto());

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetBalanceSheetsTest()
        {
            var date = DateTime.Now;
            var balanceSheetId = 1;

            var expected = new List<BalanceSheetDto>()
            {
                new BalanceSheetDto()
                {
                    Date = date,
                    BalanceSheetId = balanceSheetId
                }
            };

            var balanceSheetLinks = new List<BalanceSheetLinkItem>()
            {
                new BalanceSheetLinkItem()
                {
                    Date = date,
                    BalanceSheetId = balanceSheetId
                }
            };

            _context.Setup(x => x.GetBalanceSheetDatesAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(balanceSheetLinks.AsEnumerable()));

            var result = await _balanceSheetBL.GetBalanceSheets(_userId, _count);

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public async Task GetLiabilityChartTest()
        {
            var date = DateTime.Now;
            var totalLiabilities = 1m;

            var expected = new List<LiabilityChartDto>()
            {
                new LiabilityChartDto()
                {
                    Date = date,
                    TotalLiabilities = totalLiabilities
                }
            };

            var liabilityModels = new List<LiabilityChartItem>()
            {
                new LiabilityChartItem()
                {
                    Date = date,
                    TotalLiabilities = totalLiabilities
                }
            };

            _context.Setup(x => x.GetLiabilitiesAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(liabilityModels.AsEnumerable()));

            var result = await _balanceSheetBL.GetLiabilityChart(_userId, _count);

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, _loggerFactory);

            return config.CreateMapper();
        }
    }
}
