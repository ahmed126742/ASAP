namespace ASAP.Application.Services.ContractItems.DTOs.Processing
{
    public class GetFilteredContractItemRequest
    {
        public Guid? ContractId  { get; set; }

        public DateTime? InstallationDateFrom { get; set; }

        public DateTime? InstallationDateTo { get; set; }
    }
}
