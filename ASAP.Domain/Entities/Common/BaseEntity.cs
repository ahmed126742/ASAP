namespace ASAP.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool? Deleted { get; set; }
    }
}
