using ASAP.Application.Services.User.ServiceCall.DTOs.Processing;
using ASAP.Application.Services.User.ServiceCall.DTOs.Retireval;
using AutoMapper;

namespace ASAP.Application.Services.User.ServiceCall.Mapping
{
    public class ServiceCallProfile : Profile
    {
        public ServiceCallProfile()
        {
            CreateMap<CreateServiceCallRequest, Domain.Entities.ServiceCall>();
            CreateMap<CompleteMyServiceCallRequest, Domain.Entities.ServiceCall>();
            CreateMap<UpdateServiceCallRequest, Domain.Entities.ServiceCall>();
            CreateMap<Domain.Entities.ServiceCall, GetServiceCallResponse>();
            CreateMap<Domain.Entities.ServiceCall, GetServiceCallJobsResponse>()
                .IncludeMembers(src=>src.ContractItem);


            CreateMap<Domain.Entities.ContractItem, GetServiceCallJobsResponse>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.PostCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dst => dst.StartDate, opt => opt.MapFrom(src => src.InstallationDateFrom))
                .ForMember(dst => dst.EndDate, opt => opt.MapFrom(src => src.InstallationDateTo))
                .ForMember(dst => dst.JobTypeId, opt => opt.MapFrom(src => src.RequirementContractTypeId))
                .ForMember(dst => dst.JobId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Customer, opt => opt.MapFrom(src => src.Contract.Name));
        }
    }
}
