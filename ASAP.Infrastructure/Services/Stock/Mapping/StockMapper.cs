using ASAP.Application.Services.Stock.DTOs;
using AutoMapper;

namespace ASAP.Infrastructure.Services.Stock.Mapping
{
    public class StockMapper : Profile
    {
        public StockMapper()
        {
            CreateMap<StockRequestDto, Domain.Entities.Stock>();
        }
    }
}
