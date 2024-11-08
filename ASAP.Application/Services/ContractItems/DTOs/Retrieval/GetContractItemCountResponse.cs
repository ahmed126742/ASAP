namespace ASAP.Application.Services.ContractItems.DTOs.Retrieval
{
    public class GetContractItemCountResponse
    {
        public int ViewAll { get; set; }
        public int ViewCompleted { get; set; }
        public int ViewRemake { get; set; }
        public int ViewInComplete { get; set; }
        public int ViewOnHold { get; set; }

    }
}
