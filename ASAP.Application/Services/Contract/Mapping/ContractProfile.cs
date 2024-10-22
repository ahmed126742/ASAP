using ASAP.Application.Services.Contract.DTOs.Processing;
using ASAP.Application.Services.Contract.DTOs.Retreival;
using ASAP.Domain.Entities;
using AutoMapper;

namespace ASAP.Application.Services.Contract.Mapping
{
    internal class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<CreateContractRequest, Domain.Entities.Contract>();
            CreateMap<Domain.Entities.Contract, GetContractResponse>();
            CreateMap<Domain.Entities.Contract, GetFilteredContractsResponse>();
            CreateMap<UpdateContractRequest, Domain.Entities.Contract>();
        }
    }
}
