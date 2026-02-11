using AutoMapper;
using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DataAccess;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
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
        private Mock<IBalanceSheetContext> _context;
        private IMapper _mapper;
        private ILoggerFactory _loggerFactory;
        private int _userId = 1;
        private int _count = 1;
        private int _balanceSheetId = 1;
        private string[] _assetCategories = { "Bonds", "Cash", "Precious Metals", "Real Estate", "Stocks" };

        [TestInitialize]
        public void CreateBusinessLogic()
        {
            _context = new Mock<IBalanceSheetContext>();
            _loggerFactory = new NullLoggerFactory();
            _mapper = CreateMapper();          
            _balanceSheetBL = new BalanceSheetBL(_context.Object, _mapper);
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
                    NetWorth = 1213360.81m
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
                                    Value = 17471.98m
                                },
                                new AssetPortion()
                                {
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
                                    Value = 24524.4m
                                },
                                new AssetPortion()
                                {
                                    Value = 4236.89m
                                },
                                new AssetPortion()
                                {                                    
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
                                    Value = 5637.82m
                                },
                                new AssetPortion()
                                {
                                    Value = 11275.64m
                                },
                                new AssetPortion()
                                {
                                    Value = 5637.82m
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
                                    Value = 41379.79m
                                },
                                new AssetPortion()
                                {
                                    Value = 20689.89m
                                },
                                new AssetPortion()
                                {
                                    Value = 41379.79m
                                },
                                new AssetPortion()
                                {
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
                                    Value = 4775m
                                },
                                new AssetPortion()
                                {
                                    Value = 28.9m
                                },
                                new AssetPortion()
                                {
                                    Value = 18830.81m
                                },
                                new AssetPortion()
                                {
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
                                    Value = 4775m
                                },
                                new AssetPortion()
                                {
                                    Value = 28.9m
                                },
                                new AssetPortion()
                                {
                                    Value = 18830.81m
                                },
                                new AssetPortion()
                                {
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
                                    Value = 169900m
                                }
                            }
                        }
                    },
                    Bullion = new List<MetalPosition>()
                    {
                        new MetalPosition()
                        {
                            NumOunces = 5m,
                            PricePerOunce = 4987.1m
                        },
                        new MetalPosition()
                        {
                            NumOunces = 245m,
                            PricePerOunce = 94.34m
                        },
                        new MetalPosition()
                        {
                            NumOunces = 10m,
                            PricePerOunce = 2219m
                        },
                        new MetalPosition()
                        {
                            NumOunces = 5m,
                            PricePerOunce = 1755m
                        },
                        new MetalPosition()
                        {
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

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public async Task HasTargetsTest(bool expected)
        {
            _context.Setup(x => x.HasTargetsAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(expected));

            var result = await _balanceSheetBL.HasTargets(_userId);

            Assert.AreEqual(expected, result);
;       }

        [TestMethod]
        public async Task SetTargetsTest()
        {
            var targetList = new List<TargetDto>();
            var expected = true;

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
                        AssetComponents = new List<AssetComponentDto>(),
                        Name = "Home"
                    }
                },
                AssetTypes = new List<AssetTypeDto>()
                {
                    new AssetTypeDto()
                    {
                        Name = "Bonds"
                    },
                    new AssetTypeDto()
                    {
                        Name = "Cash"
                    },
                    new AssetTypeDto()
                    {
                        Name = "Precious Metals"
                    },
                    new AssetTypeDto()
                    {
                        Name = "Real Estate"
                    },
                    new AssetTypeDto()
                    {
                        Name = "Stocks"
                    }
                },
                BullionSummary = new BullionSummaryDto()
                {
                    Bullion = new List<MetalDto>()
                },
                Date = date,
                Liabilities = new List<LiabilityDto>(),
                Targets = new List<TargetDto>()
            };

            _context.Setup(x => x.GetDetailsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(details));

            var result = await _balanceSheetBL.GetDetails(_userId, balanceSheetId);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetBalanceSheetForEditingTest()
        {
            var balanceSheet = new BalanceSheet()
            {
                BalanceSheetId = _balanceSheetId
            };

            var expected = new BalanceSheetEditDto()
            {
                Assets = new List<AssetDto>(),
                BalanceSheetId = _balanceSheetId,
                Bullion = new List<MetalDto>(),
                Liabilities = new List<LiabilityDto>()
            };

            _context.Setup(x => x.GetBalanceSheetAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(balanceSheet));

            var result = await _balanceSheetBL.GetBalanceSheetForEditing(_userId, _balanceSheetId);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetBalanceSheetForCreatingTest()
        {
            var balanceSheet = new BalanceSheet()
            {
                BalanceSheetId = _balanceSheetId
            };

            var expected = new BalanceSheetEditDto()
            {
                Assets = new List<AssetDto>(),
                BalanceSheetId = _balanceSheetId,
                Bullion = new List<MetalDto>(),
                Liabilities = new List<LiabilityDto>()
            };

            _context.Setup(x => x.GetLatestAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(balanceSheet));

            var result = await _balanceSheetBL.GetBalanceSheetForCreating(_userId);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task EditBalanceSheetTest()
        {
            var balanceSheetEditDto = new BalanceSheetEditDto();

            var expected = true;

            _context.Setup(x => x.EditBalanceSheetAsync(It.IsAny<BalanceSheet>()))
                .Returns(Task.FromResult(expected));

            var result = await _balanceSheetBL.EditBalanceSheet(_userId, balanceSheetEditDto);

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
