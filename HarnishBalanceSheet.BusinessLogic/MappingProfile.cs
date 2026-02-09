using AutoMapper;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;

namespace HarnishBalanceSheet.BusinessLogic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AssetPortion, AssetComponentDto>()
                .ForMember(dest => dest.AssetComponentId, opt => opt.MapFrom(src => src.AssetPortionId));
            CreateMap<Asset, AssetDto>()
               .ForMember(dest => dest.AssetComponents, opt => opt.MapFrom(src => src.AssetPortions));
            CreateMap<Metal, MetalDto>();
            CreateMap<Liability, LiabilityDto>();
            CreateMap<BalanceSheet, BalanceSheetEditDto>();            
            CreateMap<BalanceSheet, DetailsDto>()
                .ForPath(dest => dest.Assets, opt => opt.MapFrom(src => src.Assets))
                .ForPath(dest => dest.Liabilities, opt => opt.MapFrom(src => src.Liabilities))
                .ForPath(dest => dest.BullionSummary.Bullion, opt => opt.MapFrom(src => src.Bullion));
            CreateMap<Target, TargetDto>()
                .ForPath(dest => dest.TargetName, opt => opt.MapFrom(src => src.AssetCategory.Name));
            CreateMap<BalanceSheetLinkItem, BalanceSheetDto>();
            CreateMap<Details, DetailsDto>()
                .ForPath(dest => dest.Assets, opt => opt.MapFrom(src => src.BalanceSheet.Assets))
                .ForPath(dest => dest.BullionSummary.Bullion, opt => opt.MapFrom(src => src.BalanceSheet.Bullion))
                .ForPath(dest => dest.Liabilities, opt => opt.MapFrom(src => src.BalanceSheet.Liabilities))
                .ForPath(dest => dest.AssetTypes, opt => opt.MapFrom(src => src.AssetTypes))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.BalanceSheet.Date));
            CreateMap<LiabilityChartItem, LiabilityChartDto>();

            CreateMap<TargetDto, Target>();
            CreateMap<AssetComponentDto, AssetPortion>();
            CreateMap<IEnumerable<AssetComponentDto>, IEnumerable<AssetPortion>>();
            CreateMap<AssetDto, Asset>()
                .ForMember(dest => dest.AssetPortions, opt => opt.MapFrom(src => src.AssetComponents));
            CreateMap<IEnumerable<AssetDto>, IEnumerable<Asset>>();
            CreateMap<MetalDto, Metal>();
            CreateMap<BalanceSheetEditDto, BalanceSheet>()
                .ForMember(dest => dest.Assets, opt => opt.MapFrom(src => src.Assets))
                .ForMember(dest => dest.Liabilities, opt => opt.MapFrom(src => src.Liabilities))
                .ForMember(dest => dest.Bullion, opt => opt.MapFrom(src => src.Bullion));
        }
    }
}
