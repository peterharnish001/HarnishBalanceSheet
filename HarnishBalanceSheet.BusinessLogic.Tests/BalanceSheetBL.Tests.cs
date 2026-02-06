using AutoMapper;
using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DataAccess;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
using HarnishBalanceSheet.PreciousMetalsService;
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
        private int _userId = 1;

        [TestInitialize]
        public void CreateBusinessLogic()
        {
            _context = new Mock<IBalanceSheetContext>();
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

        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, null);

            return config.CreateMapper();
        }
    }
}
