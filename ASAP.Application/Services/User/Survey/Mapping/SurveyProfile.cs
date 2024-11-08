using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Survey.DTOs.Processing;
using ASAP.Application.Services.User.Survey.DTOs.Retrieval;
using AutoMapper;

namespace ASAP.Application.Services.User.Survey.Mapping
{
    public class SurveyProfile : Profile
    {
        public SurveyProfile()
        {

            CreateMap<CreateSurveyRequest, Domain.Entities.Survey>();
            CreateMap<UpdateSurveyRequest, Domain.Entities.Survey>();
            CreateMap<Domain.Entities.Survey, GetSurveyResponse>();
            CreateMap<Domain.Entities.ContractItem, GetUserJobsResponse>()
                .ForMember(dst => dst.PostCode, opt => opt.MapFrom(src => src.Contract.PostCode))
                .ForMember(dst => dst.StartDate, opt => opt.MapFrom(src => src.InstallationDateFrom))
                .ForMember(dst => dst.EndDate, opt => opt.MapFrom(src => src.InstallationDateTo))
                 .ForMember(dst => dst.SurveyDateFrom, opt => opt.MapFrom(src => src.SurveyDateFrom))
                .ForMember(dst => dst.SurveyDateTo, opt => opt.MapFrom(src => src.SurveyDateTo))
                .ForMember(dst => dst.JobTypeId, opt => opt.MapFrom(src => src.RequirementContractTypeId))
                .ForMember(dst => dst.JobId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Customer, opt => opt.MapFrom(src => src.Contract.Name));

        }
    }
}
