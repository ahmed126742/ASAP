using ASAP.Application.Services.Contractor.DTOs.Processing;
using ASAP.Application.Services.Contractor.DTOs.Retreival;
using ASAP.Application.Services.Supplier.DTOs.Processing;
using ASAP.Application.Services.Supplier.DTOs.Retreival;
using AutoMapper;

namespace ASAP.Application.Services.Contractor.Mapping
{
    public class ContractorProfile : Profile
    {
        public ContractorProfile()
        {
            CreateMap<CreateContractorDto, Domain.Entities.Contractor>();
            CreateMap<UpdateContractorDto, Domain.Entities.Contractor>();
            CreateMap<Domain.Entities.Contractor, GetContractorResponse>();
            CreateMap<Domain.Entities.Contractor, GetFilteredContractorsResponse>();
        }
    }
}
