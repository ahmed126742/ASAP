using ASAP.Application.Common.Enums;

namespace ASAP.Application.Services.Contract.DTOs.Retreival
{
    public class GetFilteredContractsResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public ContractTypeEnum ContractTypeId { get; set; }

        public int ContractItemsCount { get; set; }

        public int CompletionCount  { get; set; }

        public string? Status { get; set; }
    }
}
