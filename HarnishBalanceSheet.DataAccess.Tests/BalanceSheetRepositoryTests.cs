using HarnishBalanceSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

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

        [TestMethod]
        public async Task CreateBalanceSheetAsyncTest()
        {
            var balanceSheet = new BalanceSheet()
            {
                Assets = new List<Asset>()
                {
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 4,
                                Value = 407416m
                            }
                        },
                        IsPercent = false,
                        Name = "Home"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 7035.65m
                            }
                        },
                        IsPercent = false,
                        Name = "Checking"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 14635.9m
                            }
                        },
                        IsPercent = false,
                        Name = "Savings"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 30234.13m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 273626.47m
                            }
                        },
                        IsPercent = true,
                        Name = "Fidelity"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 2850.48m
                            }
                        },
                        IsPercent = false,
                        Name = "Roth"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 24524.4m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 4236.89m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 4101.9m
                            }
                        },
                        IsPercent = false,
                        Name = "Schwab"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 5637.82m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 4,
                                Value = 5637.82m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 11275.64m
                            }
                        },
                        IsPercent = true,
                        Name = "DRS"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 41379.79m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 20689.89m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 4,
                                Value = 41379.79m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 84640.47m
                            }
                        },
                        IsPercent = true,
                        Name = "Pete's 401k"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 22551.28m
                            }
                        },
                        IsPercent = false,
                        Name = "Bitcoin"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 4775m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 28.9m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 3,
                                Value = 18830.81m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 9523.96m
                            }
                        },
                        IsPercent = false,
                        Name = "Pete's Roth"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 4775m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 28.9m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 3,
                                Value = 18830.81m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 9523.96m
                            }
                        },
                        IsPercent = false,
                        Name = "Kerry's Roth"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 5062.57m
                            }
                        },
                        IsPercent = false,
                        Name = "HSA"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 3,
                                Value = 1042.57m
                            }
                        },
                        IsPercent = false,
                        Name = "GlintPay"
                    },
                    new Asset()
                    {
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 4,
                                Value = 169900m
                            }
                        },
                        IsPercent = false,
                        Name = "Second Property"
                    }
                },
                Bullion = new List<MetalPosition>()
                { 
                    new MetalPosition()
                    {
                        NumOunces = 5,
                        PreciousMetalId = 1,
                        PricePerOunce = 4987.1m
                    },
                    new MetalPosition()
                    {
                        NumOunces = 245,
                        PreciousMetalId = 2,
                        PricePerOunce = 94.34m
                    },
                    new MetalPosition()
                    {
                        NumOunces = 10,
                        PreciousMetalId = 3,
                        PricePerOunce = 2219m
                    },
                    new MetalPosition()
                    {
                        NumOunces = 5,
                        PreciousMetalId = 4,
                        PricePerOunce = 1755m
                    },
                    new MetalPosition()
                    {
                        NumOunces = 2,
                        PreciousMetalId = 5,
                        PricePerOunce = 10500m
                    }
                },
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
                },
                UserId = _userId
            };

            var result = await _repository.CreateBalanceSheetAsync(balanceSheet);

            Assert.IsGreaterThan(0, result);
        }

        [TestMethod]
        public async Task GetBalanceSheetDatesAsyncTest()
        {
            var result = await _repository.GetBalanceSheetDatesAsync(_userId, 1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetLiabilitiesAsyncTest()
        {
            var result = await _repository.GetLiabilitiesAsync(_userId, 1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetNetWorthChartModelsAsyncTest()
        {
            var result = await _repository.GetNetWorthChartModelsAsync(_userId, 1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetDetailsAsyncTest()
        {
            var result = await _repository.GetDetailsAsync(_userId, 1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.AssetTypes);
            Assert.IsNotNull(result.BalanceSheet);
            Assert.IsNotNull(result.BalanceSheet.Assets);
            Assert.IsNotNull(result.BalanceSheet.Bullion);
            Assert.IsGreaterThan(new DateTime(), result.BalanceSheet.Date);
            Assert.IsNotNull(result.BalanceSheet.Liabilities);
            Assert.IsNotNull(result.MetalTypes);
            Assert.IsNotNull(result.Targets);
        }

        [TestMethod]
        public async Task GetEditModelAsyncTest()
        {
            var result = await _repository.GetEditModelAsync(_userId, 1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.AssetTypes);
            Assert.IsNotNull(result.BalanceSheet);
            Assert.IsNotNull(result.BalanceSheet.Assets);
            Assert.IsNotNull(result.BalanceSheet.Bullion);
            Assert.IsGreaterThan(new DateTime(), result.BalanceSheet.Date);
            Assert.IsNotNull(result.BalanceSheet.Liabilities);
            Assert.IsNotNull(result.MetalTypes);
        }

        [TestMethod]
        public async Task GetLatestAsyncTest()
        {
            var result = await _repository.GetLatestAsync(_userId);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.AssetTypes);
            Assert.IsNotNull(result.BalanceSheet);
            Assert.IsNotNull(result.BalanceSheet.Assets);
            Assert.IsNotNull(result.BalanceSheet.Bullion);
            Assert.IsNotNull(result.BalanceSheet.Liabilities);
            Assert.IsNotNull(result.MetalTypes);
        }

        [TestMethod]
        public async Task EditBalanceSheetAsyncTest()
        {
            var balanceSheet = new BalanceSheet()
            {
                Assets = new List<Asset>()
                {
                    new Asset()
                    {
                        AssetId = 6,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 4,
                                Value = 407416m
                            }
                        },
                        IsPercent = false,
                        Name = "Home"
                    },
                    new Asset()
                    {
                        AssetId = 2,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 7035.65m
                            }
                        },
                        IsPercent = false,
                        Name = "Checking"
                    },
                    new Asset()
                    {
                        AssetId = 12,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 14635.9m
                            }
                        },
                        IsPercent = false,
                        Name = "Savings"
                    },
                    new Asset()
                    {
                        AssetId = 4,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 30234.13m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 273626.47m
                            }
                        },
                        IsPercent = true,
                        Name = "Fidelity"
                    },
                    new Asset()
                    {
                        AssetId = 11,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 2850.48m
                            }
                        },
                        IsPercent = false,
                        Name = "Roth"
                    },
                    new Asset()
                    {
                        AssetId = 13,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 24524.4m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 4236.89m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 4101.9m
                            }
                        },
                        IsPercent = false,
                        Name = "Schwab"
                    },
                    new Asset()
                    {
                        AssetId = 3,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 5637.82m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 4,
                                Value = 5637.82m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 11275.64m
                            }
                        },
                        IsPercent = true,
                        Name = "DRS"
                    },
                    new Asset()
                    {
                        AssetId = 9,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 41379.79m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 20689.89m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 4,
                                Value = 41379.79m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 84640.47m
                            }
                        },
                        IsPercent = true,
                        Name = "Pete's 401k"
                    },
                    new Asset()
                    {
                        AssetId = 1,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 22551.28m
                            }
                        },
                        IsPercent = false,
                        Name = "Bitcoin"
                    },
                    new Asset()
                    {
                        AssetId = 10,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 4775m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 28.9m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 3,
                                Value = 18830.81m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 9523.96m
                            }
                        },
                        IsPercent = false,
                        Name = "Pete's Roth"
                    },
                    new Asset()
                    {
                        AssetId = 8,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 1,
                                Value = 4775m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 28.9m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 3,
                                Value = 18830.81m
                            },
                            new AssetPortion()
                            {
                                AssetCategoryId = 5,
                                Value = 9523.96m
                            }
                        },
                        IsPercent = false,
                        Name = "Kerry's Roth"
                    },
                    new Asset()
                    {
                        AssetId = 7,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 2,
                                Value = 5062.57m
                            }
                        },
                        IsPercent = false,
                        Name = "HSA"
                    },
                    new Asset()
                    {
                        AssetId = 5,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 3,
                                Value = 1042.57m
                            }
                        },
                        IsPercent = false,
                        Name = "GlintPay"
                    },
                    new Asset()
                    {
                        AssetId = 14,
                        AssetPortions = new List<AssetPortion>()
                        {
                            new AssetPortion()
                            {
                                AssetCategoryId = 4,
                                Value = 169900m
                            }
                        },
                        IsPercent = false,
                        Name = "Second Property"
                    }
                },
                BalanceSheetId = 1,
                Bullion = new List<MetalPosition>()
                {
                    new MetalPosition()
                    {
                        MetalPositionId = 1,
                        NumOunces = 5,
                        PreciousMetalId = 1,
                        PricePerOunce = 4987.1m
                    },
                    new MetalPosition()
                    {
                        MetalPositionId = 2,
                        NumOunces = 245,
                        PreciousMetalId = 2,
                        PricePerOunce = 94.34m
                    },
                    new MetalPosition()
                    {
                        MetalPositionId = 3,
                        NumOunces = 10,
                        PreciousMetalId = 3,
                        PricePerOunce = 2219m
                    },
                    new MetalPosition()
                    {
                        MetalPositionId= 4,
                        NumOunces = 5,
                        PreciousMetalId = 4,
                        PricePerOunce = 1755m
                    },
                    new MetalPosition()
                    {
                        MetalPositionId = 5,
                        NumOunces = 2,
                        PreciousMetalId = 5,
                        PricePerOunce = 10500m
                    }
                },
                Liabilities = new List<Liability>()
                {
                    new Liability()
                    {
                        LiabilityId = 1,
                        Name = "Mortgage",
                        Value = 114886.11m
                    },
                    new Liability()
                    {
                        LiabilityId = 2,
                        Name = "Credit Card",
                        Value = 2816.01m
                    }
                },
                UserId = _userId
            };

            var result = await _repository.EditBalanceSheetAsync(balanceSheet);

            Assert.IsGreaterThan(0, result);
        }
    }
}
