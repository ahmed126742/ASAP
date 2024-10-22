using ASAP.Application.Common.Enums;

namespace ASAP.Application.Services.Supplier.DTOs.Processing
{
    public class GetFilteredSuppliersRequest
    {
        public string? SearchText { get; set; }

        public SupplierTypeEnum? SupplierTypeId { get; set; }
    }
}
