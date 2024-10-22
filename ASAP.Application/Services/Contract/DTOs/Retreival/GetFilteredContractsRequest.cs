using ASAP.Application.Common.Enums;

namespace ASAP.Application.Services.Contract.DTOs.Retreival
{
    public class GetFilteredContractsRequest
    {
        public string? SearchText { get; set; }

        public ContractTypeEnum ContractTypeId { get; set; }

        public string? Address { get; set; }
    }
}
