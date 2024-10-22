using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }

        public  string Email { get; set; }

        public string Phone { get; set; }

        public int SupplierTypeId { get; set; }

        public int? Sorted { get; set; }

    }
}
