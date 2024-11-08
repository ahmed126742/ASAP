using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Fitting.DTOs.Processing;
using ASAP.Application.Services.User.Fitting.DTOs.Retrieval;
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
            CreateMap<Domain.Entities.Fitting, GetUserJobsResponse>();
        }
    }
}
