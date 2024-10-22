using ASAP.Application.Services.ContractItems.DTOs.Processing;
using ASAP.Application.Services.ContractItems.DTOs.Retrieval;
using ASAP.Domain.Entities;
using AutoMapper;

namespace ASAP.Application.Services.ContractItems.Mapping
{
    internal class ContractItemProfile : Profile
    {
        public ContractItemProfile()
        {
            CreateMap<CreateContractItemRequest, ContractItem>();
            CreateMap<UpdateContractItemRequest, ContractItem>();
            CreateMap<ContractItem, GetContractItemResponse>();
            CreateMap<ContractItem, GetFilteredContractItemReponse>();
        }
    }
}
