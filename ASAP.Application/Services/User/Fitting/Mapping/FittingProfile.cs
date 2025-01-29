using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Fitting.DTOs.Processing;
using ASAP.Application.Services.User.Fitting.DTOs.Retrieval;
using ASAP.Application.Services.User.Survey.DTOs.Retrieval;
using ASAP.Domain.Entities;
using AutoMapper;

namespace ASAP.Application.Services.User.Fitting.Mapping
{
    public class FittingProfile : Profile
    {
        public FittingProfile()
        {
            CreateMap<CreateFittingRequest, Domain.Entities.Fitting>();
            CreateMap<CreateOutstandingFittingRequest, Domain.Entities.Fitting>();
            CreateMap<UpdateFittingRequest, Domain.Entities.Fitting>();
            CreateMap<Domain.Entities.Fitting, GetFittingResponse>();
            CreateMap<Domain.Entities.Fitting, GetFittingsResponse>()
             .ForMember(dst => dst.FittingDateFrom, opt => opt.MapFrom(src => src.ContractItem.InstallationDateFrom))
             .ForMember(dst => dst.FittingDateFrom, opt => opt.MapFrom(src => src.ContractItem.InstallationDateTo))
             .ForMember(dst => dst.FitterId, opt => opt.MapFrom(src => src.ContractItem.FitterId))
             .ForMember(dst => dst.PostalCode, opt => opt.MapFrom(src => src.ContractItem.PostalCode));

            CreateMap<Domain.Entities.Fitting, GetUserJobsResponse>();
        }
    }
}
