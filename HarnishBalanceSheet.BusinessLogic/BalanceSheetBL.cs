using AutoMapper;
using HarnishBalanceSheet.DataAccess;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
using HarnishBalanceSheet.PreciousMetalsService;

namespace HarnishBalanceSheet.BusinessLogic
{
    public class BalanceSheetBL : IBalanceSheetBL
    {
        private IBalanceSheetContext _balanceSheetContext;
        private IMapper _mapper;
        private IPreciousMetalsService _preciousMetalsService;
        public BalanceSheetBL(IBalanceSheetContext context, IMapper mapper, IPreciousMetalsService preciousMetalService)
        { 
            _balanceSheetContext = context;
            _mapper = mapper;
            _preciousMetalsService = preciousMetalService;
        }

        public async Task<bool> CreateBalanceSheet(int userId, BalanceSheetEditDto balanceSheetDto)
        {
            BalanceSheet balanceSheet = _mapper.Map<BalanceSheet>(balanceSheetDto);
            balanceSheet.UserId = userId;
            return await _balanceSheetContext.CreateBalanceSheetAsync(balanceSheet);
        }

        public async Task<bool> EditBalanceSheet(int userId, BalanceSheetEditDto balanceSheetDto)
        {
            BalanceSheet balanceSheet = _mapper.Map<BalanceSheet>(balanceSheetDto);
            balanceSheet.UserId = userId;
            return await _balanceSheetContext.EditBalanceSheetAsync(balanceSheet);
        }

        public async Task<BalanceSheetEditDto> GetBalanceSheetForCreating(int userId)
        {
            BalanceSheet balanceSheet = await _balanceSheetContext.GetLatestAsync(userId);
            BalanceSheetEditDto result = _mapper.Map<BalanceSheetEditDto>(balanceSheet);
            var prices = await _preciousMetalsService.GetPreciousMetalPricesAsync();
            result.Bullion.ForEach(x => x.PricePerOunce = prices.Where(y => y.Metal == x.MetalName).First().Price);
            return result;
        }

        public async Task<BalanceSheetEditDto> GetBalanceSheetForEditing(int userId, int balanceSheetId)
        {
            BalanceSheet balanceSheet = await _balanceSheetContext.GetDetailsAsync(userId, balanceSheetId);
            return _mapper.Map<BalanceSheetEditDto>(balanceSheet);
        }

        public async Task<IEnumerable<BalanceSheetDto>> GetBalanceSheets(int userId, int count)
        {
            IEnumerable<BalanceSheetLinkItem> balanceSheetLinkItems = await _balanceSheetContext.GetBalanceSheetDatesAsync(userId, count);
            return _mapper.Map<IEnumerable<BalanceSheetLinkItem>, IEnumerable<BalanceSheetDto>>(balanceSheetLinkItems);
        }

        public async Task<DetailsDto> GetDetails(int userId, int balanceSheetId)
        {
            BalanceSheet balanceSheet = await _balanceSheetContext.GetDetailsAsync(userId, balanceSheetId);
            DetailsDto details = _mapper.Map<DetailsDto>(balanceSheet);
            details.Assets.ForEach(x => x.Value = x.AssetComponents.Select(y => y.Value).Sum());
            details.BullionSummary.Bullion.ForEach(x => x.TotalPrice = x.NumOunces * x.PricePerOunce);
            details.BullionSummary.Total = details.BullionSummary.Bullion.Select(x => x.TotalPrice).Sum();
            details.Assets.Add(new AssetDto() 
            { 
                Name = "Coins",
                Value = details.BullionSummary.Total,
                AssetComponents = new List<AssetComponentDto>()
                {
                    new AssetComponentDto() { AssetCategory = "Precious Metals", Value = details.BullionSummary.Total }
                }
            });
            details.TotalAssets = details.Assets.Select(x => x.Value).Sum();
            details.TotalLiabilities = details.Liabilities.Select(x => x.Value).Sum();
            details.NetWorth = details.TotalAssets - details.TotalLiabilities;
            details.AssetShares = details.AssetTypes.Select(x => new AssetShareDto()
            {
                Name = x,
                AssetComponents = details.Assets.SelectMany(y => y.AssetComponents.Where(z => z.AssetCategory == x)).ToList(),
                Total = details.Assets.SelectMany(y => y.AssetComponents.Where(z => z.AssetCategory == x)).Select(a => a.Value).Sum()
            });
            details.TargetComparisons = details.Targets.Select(x => new TargetComparisonDto()
            {
                Name = x.TargetName,
                Target = x.Percentage,
                Actual = details.AssetShares.Where(y => y.Name == x.TargetName).First().Total / details.NetWorth
            }).ToList();
            details.TargetComparisons.ForEach(x => x.Difference = x.Actual / x.Target - 100);

            return details;
        }

        public async Task<IEnumerable<LiabilityChartDto>> GetLiabilityChart(int userId, int count)
        {
            var liabilityTotals = await _balanceSheetContext.GetLiabilitiesAsync(userId, count);
            return _mapper.Map<IEnumerable<LiabilityChartDto>>(liabilityTotals);
        }

        public async Task<IEnumerable<NetWorthChartDto>> GetNetWorthChart(int userId, int count)
        {
            var balanceSheets = await _balanceSheetContext.GetBalanceSheetsAsync(userId, count);
            var detailsList = new List<DetailsDto>();
            foreach (var balanceSheet in balanceSheets)
            {
                detailsList.Add(_mapper.Map<DetailsDto>(balanceSheet));
            }
            return detailsList.Select(x => new NetWorthChartDto()
            {
                Date = x.Date,
                NetWorth = CalculateNetWorth(x)
            }).ToList();
        }        

        public async Task<bool> HasTargets(int userId)
        {
            return await _balanceSheetContext.HasTargetsAsync(userId);
        }

        public async Task<bool> SetTargets(int userId, IEnumerable<TargetDto> targets)
        {
            var targetModels = _mapper.Map<IEnumerable<Target>>(targets);
            return await _balanceSheetContext.SetTargetsAsync(userId, targetModels);
        }

        private decimal CalculateNetWorth(DetailsDto details)
        {            
            details.Assets.ForEach(x => x.Value = x.AssetComponents.Select(y => y.Value).Sum());
            details.BullionSummary.Bullion.ForEach(x => x.TotalPrice = x.NumOunces * x.PricePerOunce);
            details.BullionSummary.Total = details.BullionSummary.Bullion.Select(x => x.TotalPrice).Sum();
            details.Assets.Add(new AssetDto()
            {
                Name = "Coins",
                Value = details.BullionSummary.Total,
                AssetComponents = new List<AssetComponentDto>()
                {
                    new AssetComponentDto() { AssetCategory = "Precious Metals", Value = details.BullionSummary.Total }
                }
            });
            details.TotalAssets = details.Assets.Select(x => x.Value).Sum();
            details.TotalLiabilities = details.Liabilities.Select(x => x.Value).Sum();
            return details.TotalAssets - details.TotalLiabilities;
        }
    }
}
