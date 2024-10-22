using ASAP.Application.Services.Supplier.DTOs.Processing;
using ASAP.Application.Services.Supplier.DTOs.Retreival;
using AutoMapper;

namespace ASAP.Application.Services.Supplier.Mapping
{
    public class SupplierMapping : Profile
    {
        public SupplierMapping()
        {
            CreateMap<CreateSupplierRequest, Domain.Entities.Supplier>();
            CreateMap<UpdateSupplierRequest, Domain.Entities.Supplier>();
            CreateMap<Domain.Entities.Supplier, GetSupplierResponse>();
            CreateMap<Domain.Entities.Supplier, GetFilteredSuppliersResponse>();
        }
    }
}
