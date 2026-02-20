using AutoMapper;
using HarnishBalanceSheet.DataAccess;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
using HarnishBalanceSheet.PreciousMetalsService;

namespace HarnishBalanceSheet.BusinessLogic
{
    public class BalanceSheetBL : IBalanceSheetBL
    {
        private IBalanceSheetRepository _balanceSheetContext;
        private IPreciousMetalsService _preciousMetalsService;
        private IMapper _mapper;
        public BalanceSheetBL(IBalanceSheetRepository context, IMapper mapper, IPreciousMetalsService preciousMetalsService)
        { 
            _balanceSheetContext = context;
            _mapper = mapper;
            _preciousMetalsService = preciousMetalsService;
        }

        public async Task<int> CreateBalanceSheet(int userId, BalanceSheetSaveDto balanceSheetSaveDto)
        {
            BalanceSheet balanceSheet = _mapper.Map<BalanceSheet>(balanceSheetSaveDto);
            return await _balanceSheetContext.CreateBalanceSheetAsync(userId, balanceSheet);
        }

        public async Task<int> EditBalanceSheet(int userId, int balanceSheetId, BalanceSheetSaveDto balanceSheetDto)
        {
            BalanceSheet balanceSheet = _mapper.Map<BalanceSheet>(balanceSheetDto);
            return await _balanceSheetContext.EditBalanceSheetAsync(userId, balanceSheetId, balanceSheet);
        }

        public async Task<BalanceSheetEditDto> GetBalanceSheetForCreating(int userId)
        {
            EditModel editModel = await _balanceSheetContext.GetLatestAsync(userId);
            BalanceSheetEditDto result = _mapper.Map<BalanceSheetEditDto>(editModel);
            IEnumerable<PreciousMetalPrice> prices = await _preciousMetalsService.GetPreciousMetalsPricesAsync();
            result.Bullion.ToList().ForEach(x => x.PricePerOunce = prices.Where(y => y.Metal == x.MetalName).First().Price);
            return result;
        }

        public async Task<BalanceSheetEditDto> GetBalanceSheetForEditing(int userId, int balanceSheetId)
        {
            EditModel editModel = await _balanceSheetContext.GetEditModelAsync(userId, balanceSheetId);
            BalanceSheetEditDto result = _mapper.Map<BalanceSheetEditDto>(editModel);
            IEnumerable<PreciousMetalPrice> prices = await _preciousMetalsService.GetPreciousMetalsPricesAsync();
            result.Bullion.ToList().ForEach(x => x.PricePerOunce = prices.Where(y => y.Metal == x.MetalName).First().Price);
            return result;
        }

        public async Task<IEnumerable<BalanceSheetDto>> GetBalanceSheets(int userId, int count)
        {
            IEnumerable<BalanceSheetLinkItem> balanceSheetLinkItems = await _balanceSheetContext.GetBalanceSheetDatesAsync(userId, count);
            return _mapper.Map<IEnumerable<BalanceSheetLinkItem>, IEnumerable<BalanceSheetDto>>(balanceSheetLinkItems);
        }

        public async Task<DetailsDto> GetDetails(int userId, int balanceSheetId)
        {
            Details detailsModel = await _balanceSheetContext.GetDetailsAsync(userId, balanceSheetId);
            DetailsDto details = _mapper.Map<DetailsDto>(detailsModel);
            var assetPortions = detailsModel.BalanceSheet.Assets.SelectMany(x => x.AssetPortions).ToList();
            details.NetWorth = CalculateNetWorth(details, assetPortions);
            details.AssetShares = detailsModel.AssetTypes.Select(x => new AssetShareDto()
            {
                Name = x.Name,
                AssetComponents = _mapper.Map<List<AssetComponentDto>>(assetPortions.Where(y => y.AssetCategoryName == x.Name))
            });
            details.TargetComparisons = detailsModel.Targets.Select(x => new TargetComparisonDto()
            {
                Name = x.AssetCategoryName,
                Target = x.Percentage,
                Actual = details.AssetShares.Where(y => y.Name == x.AssetCategoryName).First().Total / details.NetWorth
            }).ToList();
            details.TargetComparisons.ForEach(x => x.Difference = x.Actual / x.Target - 1);

            return details;
        }

        public async Task<IEnumerable<LiabilityChartDto>> GetLiabilityChart(int userId, int count)
        {
            var liabilityTotals = await _balanceSheetContext.GetLiabilitiesAsync(userId, count);
            return _mapper.Map<IEnumerable<LiabilityChartDto>>(liabilityTotals);
        }

        public async Task<IEnumerable<NetWorthChartDto>> GetNetWorthChart(int userId, int count)
        {
            var netWorthChartModels = await _balanceSheetContext.GetNetWorthChartModelsAsync(userId, count);
            return _mapper.Map<List<NetWorthChartDto>>(netWorthChartModels);            
        }        

        public async Task<IEnumerable<AssetTypeDto>> HasTargets(int userId)
        {
            var assetCategories = await _balanceSheetContext.HasTargetsAsync(userId);
            return _mapper.Map<List<AssetTypeDto>>(assetCategories);
        }

        public async Task<int> SetTargets(int userId, IEnumerable<SetTargetDto> targets)
        {
            var targetModels = _mapper.Map<IEnumerable<Target>>(targets);
            return await _balanceSheetContext.SetTargetsAsync(userId, targetModels);
        }

        private decimal CalculateNetWorth(DetailsDto details, List<AssetPortion> assetPortions)
        {            
            details.Assets.ForEach(x => x.Value = assetPortions.Where(y => y.AssetName == x.Name).Select(z => z.Value).Sum());
            details.BullionSummary.Bullion.ForEach(x => x.TotalPrice = x.NumOunces * x.PricePerOunce);
            details.BullionSummary.Total = details.BullionSummary.Bullion.Select(x => x.TotalPrice).Sum();

            if (details.BullionSummary.Total > 0)
            {
                details.Assets.Add(new AssetDto()
                {
                    Name = "Bullion",
                    Value = details.BullionSummary.Total
                });

                assetPortions.Add(new AssetPortion()
                {
                    AssetCategoryName = "Precious Metals",
                    Value = details.BullionSummary.Total,
                    AssetName = "Bullion"
                });
            }
            details.TotalAssets = details.Assets.Select(x => x.Value).Sum();
            details.TotalLiabilities = details.Liabilities.Select(x => x.Value).Sum();
            return details.TotalAssets - details.TotalLiabilities;
        }
    }
}
