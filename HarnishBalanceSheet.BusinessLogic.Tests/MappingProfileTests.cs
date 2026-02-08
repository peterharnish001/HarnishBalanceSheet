using AutoMapper;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
using HarnishBalanceSheet.PreciousMetalsService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
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
            var assetPortion = new AssetPortion()
            {
                AssetCategory = "Real Estate",
                Value = 407416m
            };

            var expected = new AssetComponentDto()
            {
                AssetCategory = "Real Estate",
                Value = 407416m
            };

            var actual = _mapper.Map<AssetComponentDto>(assetPortion);

            Assert.AreEqual(expected, actual);
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
