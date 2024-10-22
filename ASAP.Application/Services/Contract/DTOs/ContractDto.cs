using ASAP.Application.Common.Enums;

namespace ASAP.Application.Services.Contract.DTOs
{
    public class ContractDto
    {
        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? SiteAgent { get; set; }

        public string? TLO_Name { get; set; }

        public string? TLO_Mobile { get; set; }

        public string? Address { get; set; }

        public string? PostCode { get; set; }

        public ContractTypeEnum ContractTypeId { get; set; }

        public bool IsArchived { get; set; }

        public Guid ContractorId { get; set; }
    }
}
