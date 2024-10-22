using ASAP.Application.Common.Models;
using ASAP.Application.Features.Users.GetFilteredUsers;
using ASAP.Application.Services.Supplier;
using ASAP.Application.Services.Supplier.DTOs.Processing;
using ASAP.Application.Services.Supplier.DTOs.Retreival;
using Microsoft.AspNetCore.Mvc;
namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost("CreateSupplier")]
        public async Task<ActionResult<Guid>> CreateSupplier(CreateSupplierRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _supplierService.CreateSupplierAsync(request, cancellationToken));
        }

        [HttpPost("GetSupplier")]
        public async Task<ActionResult<GetSupplierResponse>> GetSupplier(GetSupplierRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _supplierService.GetSupplierAsync(request, cancellationToken));
        }

        [HttpPost("GetSuppliers")]
        public async Task<ActionResult<GetFilteredSuppliersResponse>> GetSuppliers(PaginationRequest<GetFilteredSuppliersRequest, GetFilteredUsersResponse> request, CancellationToken cancellationToken)
        {
            return Ok(await _supplierService.GetPagedFilteresSuppliers(request, cancellationToken));
        }

        [HttpPost("UpdateSupplier")]
        public async Task<ActionResult> UpdateSupplier(UpdateSupplierRequest request, CancellationToken cancellationToken)
        {
            await _supplierService.UpdateSupplierAsync(request, cancellationToken);
            return Ok();
        }

        [HttpPost("DeleteSupplier")]
        public async Task<ActionResult> DeleteSupplier(DeleteSupplierRequest request, CancellationToken cancellationToken)
        {
            await _supplierService.DeleteSupplierAsync(request, cancellationToken);
            return Ok();
        }
    }
}
