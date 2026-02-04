using AutoMapper;
using HarnishBalanceSheet.DTO;
using HarnishBalanceSheet.Models;

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
            CreateMap<BalanceSheet, BalanceSheetDto>();
            CreateMap<IEnumerable<BalanceSheet>, IEnumerable<BalanceSheetDto>>();
            CreateMap<Liability,  LiabilityDto>();
            CreateMap<IEnumerable<Liability>, IEnumerable<LiabilityDto>>();
            CreateMap<BalanceSheet, DetailsDto>()
                .ForMember(dest => dest.Assets, opt => opt.MapFrom(src => src.Assets))
                .ForMember(dest => dest.Liabilities, opt => opt.MapFrom(src => src.Liabilities))
                .ForMember(dest => dest.Coins, opt => opt.MapFrom(src => src.Coins));
            CreateMap<IEnumerable<BalanceSheet>, IEnumerable<DetailsDto>>();
            CreateMap<Target, TargetDto>();
            CreateMap<IEnumerable<Target>, IEnumerable<TargetDto>>();

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
