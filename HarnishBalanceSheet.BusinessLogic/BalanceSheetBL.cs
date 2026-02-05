using AutoMapper;
using HarnishBalanceSheet.DataAccess;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;

namespace HarnishBalanceSheet.BusinessLogic
{
    public class BalanceSheetBL : IBalanceSheetBL
    {
        private IBalanceSheetContext _balanceSheetContext;
        private IMapper _mapper;
        public BalanceSheetBL(IBalanceSheetContext context, IMapper mapper)
        { 
            _balanceSheetContext = context;
            _mapper = mapper;
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

        public async Task<BalanceSheetEditDto> GetBalanceSheetForEditing(int userId, int balanceSheetId)
        {
            BalanceSheet balanceSheet = await _balanceSheetContext.GetDetailsAsync(userId, balanceSheetId);
            return _mapper.Map<BalanceSheetEditDto>(balanceSheet);
        }

        public async Task<IEnumerable<BalanceSheetDto>> GetBalanceSheets(int userId, int count)
        {
            IEnumerable<BalanceSheetLinkItem> balanceSheetLinkItems = await _balanceSheetContext.GetBalanceSheetDatesAsync(userId, count);
            return _mapper.Map<IEnumerable<BalanceSheetDto>>(balanceSheetLinkItems);
        }

        public async Task<DetailsDto> GetDetails(int userId, int balanceSheetId)
        {
            BalanceSheet balanceSheet = await _balanceSheetContext.GetDetailsAsync(userId, balanceSheetId);
            DetailsDto details = _mapper.Map<DetailsDto>(balanceSheet);
            details.Assets.ForEach(x => x.Value = x.AssetComponents.Select(y => y.Value).Sum());
            details.Coins.Coins.ForEach(x => x.TotalPrice = x.Ounces * x.PricePerOunce);
            details.Coins.Total = details.Coins.Coins.Select(x => x.TotalPrice).Sum();
            details.Assets.Add(new AssetDto() 
            { 
                Name = "Coins",
                Value = details.Coins.Total,
                AssetComponents = new List<AssetComponentDto>()
                {
                    new AssetComponentDto() { AssetType = "Precious Metals", Value = details.Coins.Total }
                }
            });
            details.TotalAssets = details.Assets.Select(x => x.Value).Sum() + details.Coins.Total;
            details.TotalLiabilities = details.Liabilities.Select(x => x.Value).Sum();
            details.NetWorth = details.TotalAssets - details.TotalLiabilities;
            details.AssetShares = details.AssetTypes.Select(x => new AssetShareDto()
            {
                Name = x,
                AssetComponents = details.Assets.SelectMany(y => y.AssetComponents.Where(z => z.AssetType == x)).ToList(),
                Total = details.Assets.SelectMany(y => y.AssetComponents.Where(z => z.AssetType == x)).Select(a => a.Value).Sum()
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

        public Task<IEnumerable<LiabilityChartDto>> GetLiabilityChart(int userId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NetWorthChartDto>> GetNetWorthChart(int userId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasTargets(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetTargets(int userId, IEnumerable<TargetDto> targets)
        {
            throw new NotImplementedException();
        }
    }
}
