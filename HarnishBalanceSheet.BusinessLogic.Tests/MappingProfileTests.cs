using AutoMapper;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
using HarnishBalanceSheet.PreciousMetalsService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

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

            var result = _mapper.Map<AssetDto>(asset);

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
            var balanceSheet = new BalanceSheet()
            { 
                Assets = GetAssetList(),
                BalanceSheetId = 1,
                Bullion = GetBullionList(),
                Liabilities = GetLiabilityList()
            };

            var expected = new BalanceSheetEditDto()
            {
                Assets = GetAssetDtoList(),
                BalanceSheetId = 1,
                Bullion = GetBullionDtoList(),
                Liabilities = GetLiabilityDtoList()
            };

            var result = _mapper.Map<BalanceSheetEditDto>(balanceSheet);

            Assert.AreEqual(expected, result);
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

        private List<AssetDto> GetAssetDtoList()
        {
            return new List<AssetDto>()
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

        private ICollection<Metal> GetBullionList()
        {
            return new List<Metal>()
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
                Name = "Mortgage",
                Value = 114886.11m
            };
        }

        private Liability GetLiability()
        {
            return new Liability()
            {
                Name = "Mortgage",
                Value = 114886.11m
            };
        }

        private MetalDto GetMetalDto()
        {
            return new MetalDto()
            {
                MetalName = "Gold",
                NumOunces = 5,
                PricePerOunce = 4987.1m
            };
        }

        private Metal GetMetal()
        {
            return new Metal()
            {
                MetalName = "Gold",
                NumOunces = 5,
                PricePerOunce = 4987.1m
            };
        }

        private AssetDto GetAssetDto()
        {
            return new AssetDto()
            {
                AssetId = 1,
                Name = "Home",
                AssetComponents = GetAssetComponentList()
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
                AssetCategory = "Real Estate",
                Value = 407416m
            };
        }

        private AssetPortion GetAssetPortion()
        {
            return new AssetPortion()
            {
                AssetCategory = "Real Estate",
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
