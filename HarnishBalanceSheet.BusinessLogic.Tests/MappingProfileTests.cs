using AutoMapper;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HarnishBalanceSheet.BusinessLogic.Tests
{
    [TestClass]
    public class MappingProfileTests
    {
        private IMapper _mapper;
        private ILoggerFactory _loggerFactory;

        [TestInitialize]
        public void CreateBusinessLogic()
        {
            _loggerFactory = new NullLoggerFactory();
            _mapper = CreateMapper();
        }

        [TestMethod]
        public void AssetCategoryMappingTest()
        {
            var assetPortion = GetAssetPortion();

            var expected = GetAssetComponentDto();

            var actual = _mapper.Map<AssetComponentDto>(assetPortion);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AssetCategoryListMappingTest()
        {
            var assetPortionList = GetAssetPortionList();

            var expected = GetAssetList();

            var actual = _mapper.Map<List<AssetComponentDto>>(assetPortionList);

            CollectionAssert.AreEqual(expected.ToList(), actual);
        }

        [TestMethod]
        public void AssetMappingTest()
        {
            var asset = GetAsset();

            var expected = GetAssetDto();

            var result = _mapper.Map<AssetSaveDto>(asset);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AssetListMappingTest()
        {
            var assets = GetAssetList();

            var expected = GetAssetDtoList();

            var result = _mapper.Map<List<AssetDto>>(assets);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MetalMappingTest()
        {
            var metal = GetMetal();

            var expected = GetMetalDto();

            var result = _mapper.Map<MetalDto>(metal);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BullionMappingTest()
        {
            var bullion = GetBullionList();

            var expected = GetBullionDtoList();

            var result = _mapper.Map<List<MetalDto>>(bullion);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LiabilityMappingTest()
        {
            var liability = GetLiability();

            var expected = GetLiabilityDto();

            var result = _mapper.Map<LiabilityDto>(liability);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LiabilityListMappingTest()
        {
            var liabilities = GetLiabilityList();

            var expected = GetLiabilityDtoList();

            var result = _mapper.Map<List<LiabilityDto>>(liabilities);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BalanceSheetMappingTest()
        {
            var date = DateTime.Now;
            var balanceSheet = GetBalanceSheet(date);

            var expected = new BalanceSheetEditDto()
            {
                Assets = GetAssetDtoList(),
                Bullion = GetBullionDtoList(),  
                Date = date,
                Liabilities = GetLiabilityDtoList()
            };

            var result = _mapper.Map<BalanceSheetEditDto>(balanceSheet);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DetailsDtoMappingTest()
        {
            var date = DateTime.Now;
            var balanceSheet = GetBalanceSheet(date);

            var expected = GetDetailsDto(date);

            var result = _mapper.Map<DetailsDto>(balanceSheet);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TargetDtoMappingTest()
        {
            var target = GetTarget();

            var expected = GetTargetDto();

            var result = _mapper.Map<TargetDto>(target);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TargetDtoListMappingTest()
        {
            var targets = GetTargetList();

            var expected = GetTargetDtoList();

            var result = _mapper.Map<List<TargetDto>>(targets);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BalanceSheetDtoMappingTest()
        {
            var date = DateTime.Now;
            var balanceSheetLinkItem = GetBalanceSheetLinkItem(date);

            var expected = GetBalanceSheetDto(date);

            var result = _mapper.Map<BalanceSheetDto>(balanceSheetLinkItem);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BalanceSheetDtoListMappingTest()
        {
            var date = DateTime.Now;
            var balanceSheetLinkItems = new List<BalanceSheetLinkItem>()
            {
                GetBalanceSheetLinkItem(date)
            };

            var expected = new List<BalanceSheetDto>()
            {
                GetBalanceSheetDto(date)
            };

            var result = _mapper.Map<List<BalanceSheetDto>>(balanceSheetLinkItems);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DetailsToDetailsDtoMappingTest()
        {
            var date = DateTime.Now;
            var details = new Details()
            {
                BalanceSheet = GetBalanceSheet(date),
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
                }
            };

            var expected = GetDetailsDto(date);

            var result = _mapper.Map<DetailsDto>(details);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LiabilityChartItemToLiabilityChartDtoMappingTest()
        {
            var date = DateTime.Now;
            var liabilityChartItem = GetLiabilityChartItem(date);

            var expected = GetLiabilityChartDto(date);

            var result = _mapper.Map<LiabilityChartDto>(liabilityChartItem);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LiabilityChartItemListToLiabilityChartDtoListMappingTest()
        {
            var date = DateTime.Now;
            var liabilityChartItemList = new List<LiabilityChartItem>()
            {
                GetLiabilityChartItem(date)
            };

            var expected = new List<LiabilityChartDto>()
            {
                GetLiabilityChartDto(date)
            };

            var result = _mapper.Map<List<LiabilityChartDto>>(liabilityChartItemList);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TargetDtoToTargetMappingTest()
        {
            var targetDto = GetTargetDto();

            var expected = GetTarget();

            var result = _mapper.Map<Target>(targetDto);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TargetDtoListToTargetListMappingTest()
        {
            var targetDtoList = GetTargetDtoList();

            var expected = GetTargetList();

            var result = _mapper.Map<List<Target>>(targetDtoList);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AssetComponentDtoToAssetPortionMappingTest()
        {
            var assetComponentDto = GetAssetComponentDto();

            var expected = GetAssetPortion();

            var result = _mapper.Map<AssetPortion>(assetComponentDto);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AssetComponentDtoListToAssetPortionListMappingTest()
        {
            var assetComponentDtoList = GetAssetComponentList();

            var expected = GetAssetPortionList();

            var result = _mapper.Map<List<AssetPortion>>(assetComponentDtoList);

            CollectionAssert.AreEqual(expected.ToList(), result);
        }

        [TestMethod]
        public void AssetDtoToAssetMappingTest()
        {
            var assetDto = GetAssetDto();

            var expected = GetAsset();

            var result = _mapper.Map<Asset>(assetDto);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AssetDtoListToAssetListMappingTest()
        {
            var assetDtoList = GetAssetDtoList();

            var expected = GetAssetList(); 

            var result = _mapper.Map<List<Asset>>(assetDtoList);

            CollectionAssert.AreEqual(expected.ToList(), result);
        }

        [TestMethod]
        public void MetalDtoToMetalMappingTest()
        {
            var metalDto = GetMetalDto();

            var expected = GetMetal();

            var result = _mapper.Map<MetalPosition>(metalDto);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MetalDtoListToMetalListMappingTest()
        {
            var metalDtoList = GetBullionDtoList();

            var expected = GetBullionList();

            var result = _mapper.Map<List<MetalPosition>>(metalDtoList);

            CollectionAssert.AreEqual (expected.ToList(), result);
        }

        [TestMethod]
        public void LiabilityDtoToLiabilityMappingTest()
        {
            var liabilityDto = GetLiabilityDto();

            var expected = GetLiability();

            var result = _mapper.Map<Liability>(liabilityDto);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LiabilityDtoListToLiabilityListMappingTest()
        {
            var liabilityDtoList = GetLiabilityDtoList();

            var expected = GetLiabilityList();

            var result = _mapper.Map<List<Liability>>(liabilityDtoList);

            CollectionAssert.AreEqual(expected.ToList(), result);
        }

        [TestMethod]
        public void BalanceSheetEditDtoToBalanceSheetMappingTest()
        {
            var date = DateTime.Now;
            var balanceSheetEditDto = new BalanceSheetEditDto()
            {
                Assets = GetAssetDtoList(),
                Bullion = GetBullionDtoList(),
                Date = date,
                Liabilities = GetLiabilityDtoList()
            };

            var expected = GetBalanceSheet(date);

            var result = _mapper.Map<BalanceSheet>(balanceSheetEditDto);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AssetCategoryToAssetTypeDtoMappingTest()
        {
            var assetCategory = GetAssetCategory();

            var expected = GetAssetTypeDto();

            var result = _mapper.Map<AssetTypeDto>(assetCategory);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AssetCategoryListToAssetTypeDtoListMappingTest()
        {
            var assetCategoryList = new List<AssetCategory>()
            {
                GetAssetCategory()
            };

            var expected = new List<AssetTypeDto>()
            {
                GetAssetTypeDto()
            };

            var result = _mapper.Map<List<AssetTypeDto>>(assetCategoryList);

            CollectionAssert.AreEqual(expected, result);
        }

        private AssetTypeDto GetAssetTypeDto()
        {
            return new AssetTypeDto()
            {
                Name = "Bonds"
            };
        }

        private AssetCategory GetAssetCategory()
        {
            return new AssetCategory()
            {
                Name = "Bonds"
            };
        }

        private List<TargetDto> GetTargetDtoList()
        {
            return new List<TargetDto>()
            {
                GetTargetDto()
            };
        }

        private List<Target> GetTargetList()
        {
            return new List<Target>()
            {
                GetTarget()
            };
        }

        private LiabilityChartDto GetLiabilityChartDto(DateTime date)
        {
            return new LiabilityChartDto()
            {
                Date = date,
                TotalLiabilities = 1
            };
        }

        private LiabilityChartItem GetLiabilityChartItem(DateTime date)
        {
            return new LiabilityChartItem()
            {
                Date = date,
                TotalLiabilities = 1
            };
        }

        private DetailsDto GetDetailsDto(DateTime date)
        {
            return new DetailsDto()
            {
                Assets = new List<AssetDto>(),
                BullionSummary = new BullionSummaryDto()
                {
                    Bullion = GetBullionDtoList()
                },
                Date = date,
                Liabilities = GetLiabilityDtoList()
            };
        }

        private BalanceSheetDto GetBalanceSheetDto(DateTime date)
        {
            return new BalanceSheetDto()
            {
                BalanceSheetId = 1,
                Date = date
            };
        }

        private BalanceSheetLinkItem GetBalanceSheetLinkItem(DateTime date)
        {
            return new BalanceSheetLinkItem()
            {
                BalanceSheetId = 1,
                Date = date
            };
        }

        private TargetDto GetTargetDto()
        {
            return new TargetDto()
            {
                AssetCategoryId = 1,
                TargetName = "Bonds",
                Percentage = 0.2m
            };
        }

        private Target GetTarget()
        {
            return new Target()
            {
                AssetCategory = new AssetCategory()
                {
                    Name = "Bonds"
                },
                AssetCategoryId = 1,
                Percentage = 0.2m
            };
        }

        private BalanceSheet GetBalanceSheet(DateTime date)
        {
            return new BalanceSheet()
            {
                Assets = GetAssetList(),
                BalanceSheetId = 1,
                Bullion = GetBullionList(), 
                Date = date,
                Liabilities = GetLiabilityList()
            };
        }

        private List<LiabilityDto> GetLiabilityDtoList()
        {
            return new List<LiabilityDto>()
            {
                GetLiabilityDto()
            };
        }

        private List<MetalDto> GetBullionDtoList()
        {
            return new List<MetalDto>()
            {
                GetMetalDto()
            };
        }

        private List<AssetSaveDto> GetAssetDtoList()
        {
            return new List<AssetSaveDto>()
            {
                GetAssetDto()
            };
        }

        private ICollection<Liability> GetLiabilityList()
        {
            return new List<Liability>()
            {
                GetLiability()
            };
        }

        private ICollection<MetalPosition> GetBullionList()
        {
            return new List<MetalPosition>()
            {
                GetMetal()
            };
        }

        private ICollection<Asset> GetAssetList()
        {
            return new List<Asset>()
            {
                GetAsset()
            };
        }

        private LiabilityDto GetLiabilityDto()
        {
            return new LiabilityDto()
            {
                LiabilityId = 1,
                Name = "Mortgage",
                Value = 114886.11m
            };
        }

        private Liability GetLiability()
        {
            return new Liability()
            {
                LiabilityId = 1,
                Name = "Mortgage",
                Value = 114886.11m
            };
        }

        private MetalDto GetMetalDto()
        {
            return new MetalDto()
            {
                MetalPositionId = 1,
                MetalName = "Gold",
                NumOunces = 5,
                PricePerOunce = 4987.1m
            };
        }

        private MetalPosition GetMetal()
        {
            return new MetalPosition()
            {
                MetalPositionId = 1,
                NumOunces = 5,
                PricePerOunce = 4987.1m
            };
        }

        private AssetSaveDto GetAssetDto()
        {
            return new AssetSaveDto()
            {
                AssetId = 1,
                Name = "Home"
            };
        }

        private Asset GetAsset()
        {
            return new Asset()
            {
                AssetId = 1,
                Name = "Home",
                AssetPortions = GetAssetPortionList()
            };
        }

        private IEnumerable<AssetComponentDto> GetAssetComponentList()
        {
            return new List<AssetComponentDto>()
            {
                GetAssetComponentDto()
            };
        }

        private ICollection<AssetPortion> GetAssetPortionList()
        {
            return new List<AssetPortion>()
            {
                GetAssetPortion()
            };
        }

        private AssetComponentDto GetAssetComponentDto()
        {
            return new AssetComponentDto()
            {
                AssetComponentId = 1,
                AssetCategory = "Real Estate",
                Value = 407416m
            };
        }

        private AssetPortion GetAssetPortion()
        {
            return new AssetPortion()
            {
                AssetPortionId = 1,
                Value = 407416m
            };
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
