using ASAP.Application.Common.Enums;
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
            CreateMap<CreateContractItemRequest, ContractItem>()
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => !src.Status.HasValue ? JobStatusEnum.ToSurvey : src.Status.Value));
            CreateMap<UpdateContractItemRequest, ContractItem>()
                .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status != null));
            CreateMap<ContractItem, GetContractItemResponse>()
                .ForMember(dst => dst.ContractName, opt => opt.MapFrom(src => src.Contract.Name));
            CreateMap<ContractItem, GetFilteredContractItemReponse>()
                .ForMember(dst => dst.ContractName, opt => opt.MapFrom(src => src.Contract.Name));
        }
    }
}
