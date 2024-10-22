using ASAP.Application.Common.Models;
using ASAP.Application.Features.Users.GetFilteredUsers;
using ASAP.Application.Services.Supplier.DTOs.Processing;
using ASAP.Application.Services.Supplier.DTOs.Retreival;

namespace ASAP.Application.Services.Supplier
{
    public interface ISupplierService
    {

        Task<GetSupplierResponse> GetSupplierAsync(GetSupplierRequest request, CancellationToken cancellationToken);

        Task<PagedReponse<GetFilteredSuppliersResponse>> GetPagedFilteresSuppliers(PaginationRequest<GetFilteredSuppliersRequest, GetFilteredUsersResponse> request, CancellationToken CancellationToken);

        Task<Guid> CreateSupplierAsync(CreateSupplierRequest request, CancellationToken cancellationToken);

        Task UpdateSupplierAsync(UpdateSupplierRequest request, CancellationToken cancellationToken);

        Task DeleteSupplierAsync(DeleteSupplierRequest request, CancellationToken cancellationToken);
    }
}
