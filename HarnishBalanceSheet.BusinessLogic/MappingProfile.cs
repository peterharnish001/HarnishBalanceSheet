using AutoMapper;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;
using System.Xml.Serialization;

namespace HarnishBalanceSheet.BusinessLogic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AssetPortion, AssetComponentDto>();
            CreateMap<IEnumerable<AssetPortion>, IEnumerable<AssetComponentDto>>();
            CreateMap<Asset, AssetDto>()
               .ForMember(dest => dest.AssetComponents, opt => opt.MapFrom(src => src.AssetPortions));
            CreateMap<IEnumerable<Asset>, IEnumerable<AssetDto>>();
            CreateMap<Coins, CoinDto>();
            CreateMap<IEnumerable<Coins>, IEnumerable<CoinDto>>();
            CreateMap<BalanceSheet, BalanceSheetEditDto>();
            CreateMap<Liability,  LiabilityDto>();
            CreateMap<IEnumerable<Liability>, IEnumerable<LiabilityDto>>();
            CreateMap<BalanceSheet, DetailsDto>()
                .ForMember(dest => dest.Assets, opt => opt.MapFrom(src => src.Assets))
                .ForMember(dest => dest.Liabilities, opt => opt.MapFrom(src => src.Liabilities))
                .ForMember(dest => dest.Coins, opt => opt.MapFrom(src => src.Coins));
            CreateMap<IEnumerable<BalanceSheet>, IEnumerable<DetailsDto>>();
            CreateMap<Target, TargetDto>()
                .ForMember(dest => dest.TargetName, opt => opt.MapFrom(src => src.AssetCategory.Name));
            CreateMap<IEnumerable<Target>, IEnumerable<TargetDto>>();
            CreateMap<BalanceSheetLinkItem, BalanceSheetDto>();
            CreateMap<IEnumerable<BalanceSheetLinkItem>, IEnumerable<BalanceSheetDto>>();
            CreateMap<DetailsDto, Details>()
                .ForMember(dest => dest.BalanceSheet.Assets, opt => opt.MapFrom(src => src.Assets))
                .ForMember(dest => dest.BalanceSheet.Coins, opt => opt.MapFrom(src => src.Coins))
                .ForMember(dest => dest.BalanceSheet.Liabilities, opt => opt.MapFrom(src => src.Liabilities))
                .ForMember(dest => dest.AssetTypes, opt => opt.MapFrom(src => src.AssetTypes));

            CreateMap<TargetDto, Target>();
            CreateMap<IEnumerable<TargetDto>, IEnumerable<Target>>();
            CreateMap<AssetComponentDto, AssetPortion>();
            CreateMap<IEnumerable<AssetComponentDto>, IEnumerable<AssetPortion>>();
            CreateMap<AssetDto, Asset>()
                .ForMember(dest => dest.AssetPortions, opt => opt.MapFrom(src => src.AssetComponents));
            CreateMap<IEnumerable<AssetDto>, IEnumerable<Asset>>();
            CreateMap<CoinDto, Coins>();
            CreateMap<IEnumerable<CoinDto>, IEnumerable<Coins>>();
            CreateMap<BalanceSheetEditDto, BalanceSheet>()
                .ForMember(dest => dest.Assets, opt => opt.MapFrom(src => src.Assets))
                .ForMember(dest => dest.Liabilities, opt => opt.MapFrom(src => src.Liabilities))
                .ForMember(dest => dest.Coins, opt => opt.MapFrom(src => src.Coins));
        }
    }
}
