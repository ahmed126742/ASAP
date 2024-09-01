using ASAP.Application.Services.Stock.DTOs;
using ASAP.Infrastructure.Intergrations.Dtos;
using AutoMapper;

namespace ASAP.Infrastructure.BackgroudServices.Mapping
{
    public class PolygonProfile : Profile
    {
        public PolygonProfile()
        {
            CreateMap<Polygon, StockRequestDto>()
                .ForMember(dst => dst.SymbolExchange, opt => opt.MapFrom(src => src.T))
                .ForMember(dst => dst.TimeStamp, opt => opt.MapFrom(src => src.t))
                .ForMember(dst => dst.Low, opt => opt.MapFrom(src => src.L))
                .ForMember(dst => dst.Open, opt => opt.MapFrom(src => src.o))
                .ForMember(dst => dst.TransactionNo, opt => opt.MapFrom(src => src.n))
                .ForMember(dst => dst.Close, opt => opt.MapFrom(src => src.c))
                .ForMember(dst => dst.High, opt => opt.MapFrom(src => src.h))
                .ForMember(dst => dst.Volum, opt => opt.MapFrom(src => src.v))
                .ForMember(dst => dst.VolumWeight, opt => opt.MapFrom(src => src.vw));

        }
    }
}
