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
        private string[] _assetCategories = { "Bonds", "Cash", "Precious Metals", "Real Estate", "Stocks" };

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

        [TestMethod]
        public async Task GetNetWorthChartTest()
        { 
            var date = DateTime.Now;
            var expected = new List<NetWorthChartDto>()
            {
                new NetWorthChartDto()
                {
                    Date = date,
                    NetWorth = 1226122.56m
                }
            };

            var balanceSheets = new List<BalanceSheet>()
            {
                new BalanceSheet()
                {
                    Assets = new List<Asset>()
                    {
                        new Asset()
                        {
                            Name = "Home",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[3],
                                    Value = 407416m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "Checking",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[1],
                                    Value = 7035.65m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "Savings",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[1],
                                    Value = 14635.9m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "Fidelity",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[0],
                                    Value = 17471.98m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[4],
                                    Value = 273626.47m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "Schwab",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[0],
                                    Value = 24524.4m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[1],
                                    Value = 4236.89m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[4],
                                    Value = 4101.9m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "Roth",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[1],
                                    Value = 2850.48m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "DRS",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[0],
                                    Value = 2593.4m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[3],
                                    Value = 1851.46m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[4],
                                    Value = 7901.97m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "Pete's 401k",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[0],
                                    Value = 41379.79m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[1],
                                    Value = 20689.89m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[3],
                                    Value = 41379.79m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[4],
                                    Value = 84640.87m
                                },
                            }
                        },
                        new Asset()
                        {
                            Name = "Bitcoin",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[1],
                                    Value = 22185.36m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "Pete's Roth",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[0],
                                    Value = 4775m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[1],
                                    Value = 28.9m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[2],
                                    Value = 18830.81m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[4],
                                    Value = 9523.96m
                                },
                            }
                        },
                        new Asset()
                        {
                            Name = "Kerry's Roth",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[0],
                                    Value = 4775m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[1],
                                    Value = 28.9m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[2],
                                    Value = 18830.81m
                                },
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[4],
                                    Value = 9523.96m
                                },
                            }
                        },
                        new Asset()
                        {
                            Name = "HSA",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[1],
                                    Value = 5062.57m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "GlintPay",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[2],
                                    Value = 1042.57m
                                }
                            }
                        },
                        new Asset()
                        {
                            Name = "Second Property",
                            AssetPortions = new List<AssetPortion>()
                            {
                                new AssetPortion()
                                {
                                    AssetCategory = _assetCategories[3],
                                    Value = 169900m
                                }
                            }
                        }
                    },
                    Bullion = new List<Metal>()
                    {
                        new Metal()
                        {
                            MetalName = "Gold",
                            NumOunces = 5m,
                            PricePerOunce = 4987.1m
                        },
                        new Metal()
                        {
                            MetalName = "Silver",
                            NumOunces = 245m,
                            PricePerOunce = 94.34m
                        },
                        new Metal()
                        {
                            MetalName = "Platinum",
                            NumOunces = 10m,
                            PricePerOunce = 2219m
                        },
                        new Metal()
                        {
                            MetalName = "Palladium",
                            NumOunces = 5m,
                            PricePerOunce = 1755m
                        },
                        new Metal()
                        {
                            MetalName = "Rhodium",
                            NumOunces = 2m,
                            PricePerOunce = 10500m
                        },
                    },
                    Date = date,
                    Liabilities = new List<Liability>()
                    {
                        new Liability()
                        {
                            Name = "Mortgage",
                            Value = 114886.11m
                        },
                        new Liability()
                        {
                            Name = "Credit Card",
                            Value = 2816.01m
                        }
                    }
                }
            };

            _context.Setup(x => x.GetBalanceSheetsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(balanceSheets.AsEnumerable()));

            var result = await _balanceSheetBL.GetNetWorthChart(_userId, _count);

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
