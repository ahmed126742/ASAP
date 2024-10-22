using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Entities
{
    public class ServiceCall : BaseEntity
    {
        public Guid? UserId { get; set; }

        public DateTime? RequiredDate { get; set; }

        public string? ReportedIssue { get; set; }

        public string? WorkDescription  { get; set; }

        public string? PartsRequired { get; set; }

        public bool? IsCustomerHappy { get; set; }

        public bool? WasEngineerOnTime { get; set; }

        public int? WorkQualityRate { get; set; }

        public Guid? AttachementHeaderId { get; set; }

        public User? User { get; set; }
    }
}
