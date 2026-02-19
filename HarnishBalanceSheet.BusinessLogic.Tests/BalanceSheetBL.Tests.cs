using AutoMapper;
using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DataAccess;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
using HarnishBalanceSheet.PreciousMetalsService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace HarnishBalanceSheet.BusinessLogic.Tests
{
    [TestClass]
    public sealed class BalanceSheetBLTests
    {
        private BalanceSheetBL _balanceSheetBL;
        private Mock<IBalanceSheetRepository> _context;
        private Mock<IPreciousMetalsService> _preciousMetalsService;
        private IMapper _mapper;
        private ILoggerFactory _loggerFactory;
        private int _userId = 1;
        private int _count = 1;
        private int _balanceSheetId = 1;
        private string[] _assetCategories = { "Bonds", "Cash", "Precious Metals", "Real Estate", "Stocks" };

        [TestInitialize]
        public void CreateBusinessLogic()
        {
            _context = new Mock<IBalanceSheetRepository>();
            _preciousMetalsService = new Mock<IPreciousMetalsService>();
            _loggerFactory = new NullLoggerFactory();
            _mapper = CreateMapper();          
            _balanceSheetBL = new BalanceSheetBL(_context.Object, _mapper, _preciousMetalsService.Object);
        }        

        [TestMethod]
        public async Task CreateBalanceSheetTest()
        {
            var expected = _balanceSheetId;

            _context.Setup(x => x.CreateBalanceSheetAsync(It.IsAny<int>(), It.IsAny<BalanceSheet>()))
                .Returns(Task.FromResult(expected));

            var result = await _balanceSheetBL.CreateBalanceSheet(_userId, new BalanceSheetSaveDto());

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

        [TestMethod]
        public async Task GetNetWorthChartTest()
        { 
            var date = DateTime.Now;
            var expected = new List<NetWorthChartDto>();

            var netWorthChartModels = new List<NetWorthChartModel>();            

            _context.Setup(x => x.GetNetWorthChartModelsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(netWorthChartModels.AsEnumerable()));

            var result = await _balanceSheetBL.GetNetWorthChart(_userId, _count);

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public async Task HasTargetsTest()
        {
            var expected = new List<AssetCategory>();

            _context.Setup(x => x.HasTargetsAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(expected.AsEnumerable()));

            var result = await _balanceSheetBL.HasTargets(_userId);

            CollectionAssert.AreEqual(expected, result.ToList());
;       }

        [TestMethod]
        public async Task SetTargetsTest()
        {
            var targetList = new List<SetTargetDto>();
            var expected = 1;

            _context.Setup(x => x.SetTargetsAsync(It.IsAny<int>(), It.IsAny<IEnumerable<Target>>()))
                .Returns(Task.FromResult(expected));

            var result = await _balanceSheetBL.SetTargets(_userId, targetList);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetDetailsTest()
        {
            var balanceSheetId = 1;
            var date = DateTime.Now;
            var details = new Details()
            {
                AssetTypes = new List<AssetCategory>()
                {
                    new AssetCategory()
                    {
                        Name = "Bonds"
                    },
                    new AssetCategory()
                    {
                        Name = "Cash"
                    },
                    new AssetCategory()
                    {
                        Name = "Precious Metals"
                    },
                    new AssetCategory()
                    {
                        Name = "Real Estate"
                    },
                    new AssetCategory()
                    {
                        Name = "Stocks"
                    },
                },
                BalanceSheet = new BalanceSheet()
                {
                    Assets = new List<Asset>()
                    {
                        new Asset()
                        {
                            AssetPortions = new List<AssetPortion>(),
                            Name = "Home"
                        }
                    },
                    BalanceSheetId = balanceSheetId,
                    Bullion = new List<MetalPosition>(),
                    Date = date,
                    Liabilities = new List<Liability>()
                },
                Targets = new List<Target>()
            };
            var expected = new DetailsDto()
            {
                Assets = new List<AssetDto>()
                {
                    new AssetDto()
                    {
                       
                        Name = "Home"
                    }
                },
                BullionSummary = new BullionSummaryDto()
                {
                    Bullion = new List<MetalDto>()
                },
                Date = date,
                Liabilities = new List<LiabilityDto>()
            };

            _context.Setup(x => x.GetDetailsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(details));

            var result = await _balanceSheetBL.GetDetails(_userId, balanceSheetId);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetBalanceSheetForEditingTest()
        {
            var editModel = new EditModel()
            {
                BalanceSheet = new BalanceSheet()
            };

            var expected = new BalanceSheetEditDto()
            {
                Assets = new List<AssetDto>(),
                BalanceSheetId = _balanceSheetId,
                Bullion = new List<MetalDto>(),
                Liabilities = new List<LiabilityDto>()
            };

            _context.Setup(x => x.GetEditModelAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(editModel));

            var result = await _balanceSheetBL.GetBalanceSheetForEditing(_userId, _balanceSheetId);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetBalanceSheetForCreatingTest()
        {
            var editModel = new EditModel()
            {
                BalanceSheet = new BalanceSheet()
                {
                    Assets = new List<Asset>(),
                    BalanceSheetId = _balanceSheetId,
                    Bullion = new List<MetalPosition>(),
                    Liabilities = new List<Liability>()
                }
            };

            var expected = new BalanceSheetEditDto()
            {
                Assets = new List<AssetDto>(),
                BalanceSheetId = _balanceSheetId,
                Bullion = new List<MetalDto>(),
                Liabilities = new List<LiabilityDto>()
            };

            _context.Setup(x => x.GetLatestAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(editModel));

            var result = await _balanceSheetBL.GetBalanceSheetForCreating(_userId);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task EditBalanceSheetTest()
        {
            var balanceSheetEditDto = new BalanceSheetEditDto();

            var expected = 1;

            _context.Setup(x => x.EditBalanceSheetAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<BalanceSheet>()))
                .Returns(Task.FromResult(expected));

            var result = await _balanceSheetBL.EditBalanceSheet(_userId, 1, balanceSheetEditDto);

            Assert.AreEqual(expected, result);
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
