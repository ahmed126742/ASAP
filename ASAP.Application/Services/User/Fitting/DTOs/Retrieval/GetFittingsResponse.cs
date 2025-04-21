namespace ASAP.Application.Services.User.Fitting.DTOs.Retrieval
{
    public class GetFittingsResponse : FittingDto
    {
        public Guid? Id { get; set; }
        public int? ContractItemNumber { get; set; }

        public DateTime? FittingDateFrom { get; set; }

        public DateTime? FittingDateTo { get; set; }

        public Guid? FitterId { get; set; }

        public string? FitterName { get; set; }

        public string? PostalCode { get; set; }
    }
}
