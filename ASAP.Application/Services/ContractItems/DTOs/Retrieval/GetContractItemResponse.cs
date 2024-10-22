using ASAP.Application.Common.Enums;

namespace ASAP.Application.Services.ContractItems.DTOs.Retrieval
{
    public class GetContractItemResponse : ContractItemDto
    {
        public Guid Id { get; set; }

        public JobStatusEnum ContractItemStatusId { get; set; }
    }
}
