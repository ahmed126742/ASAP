using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Entities
{
    public class Contract : BaseEntity
    {
        public Contract()
        {
            contractItems = new HashSet<ContractItem>();
        }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? SiteAgent  { get; set; }

        public string? TLO_Name  { get; set; }

        public string? TLO_Mobile { get; set; }

        public string? Address { get; set; }

        public string? PostCode { get; set; }

        public int ContractTypeId { get; set; }

        public bool IsArchived { get; set; }

        public Guid? ContractorId { get; set; }

        public Contractor? Contractor { get; set; }

        public ICollection<ContractItem> contractItems { get; set; }
    }
}
