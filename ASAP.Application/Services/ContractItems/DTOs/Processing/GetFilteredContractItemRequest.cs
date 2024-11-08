using ASAP.Application.Common.Enums;

namespace ASAP.Application.Services.ContractItems.DTOs.Processing
{
    public class GetFilteredContractItemRequest
    {
        public ContractItemCountEnum? ContractItemCountId { get; set; }
        
        public Guid? ContractId  { get; set; }

        public int? ProductionWeek { get; set; }

        public string? Address { get; set; }

        public DateTime? InstallationDateFrom { get; set; }

        public DateTime? InstallationDateTo { get; set; }
    }
}
