using ASAP.Application.Common.Enums;

namespace ASAP.Application.Services.Supplier.DTOs
{
    public class SupplierDto
    {
       
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public SupplierTypeEnum SupplierTypeId{ get; set; }

        public int? Sorted { get; set; }
    }
}
