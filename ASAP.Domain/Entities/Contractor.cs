using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Entities
{
    public class Contractor : BaseEntity
    {
        public string Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}
