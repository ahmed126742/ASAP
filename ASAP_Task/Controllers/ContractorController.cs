using System.Threading;
using ASAP.Application.Common.Models;
using ASAP.Application.Services.Contractor;
using ASAP.Application.Services.Contractor.DTOs.Processing;
using ASAP.Application.Services.Contractor.DTOs.Retreival;
using ASAP.Application.Services.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private readonly IContractorService _contractorService;

        public ContractorController(IContractorService contractorService)
        {
            _contractorService = contractorService;
        }

        [HttpPost("CreateContractor")]
        [Authorize]

        public async Task<ActionResult<Guid>> CreateContractor(CreateContractorDto request, CancellationToken cancellationToken)
        {
            return Ok(await _contractorService.CreateContractor(request, cancellationToken));
        }

        [HttpPost("GetContractor")]
        [Authorize]
        public async Task<ActionResult<GetContractorResponse>> GetContractor(GetContractorRequest request, CancellationToken cancellationToken )
        {
            return Ok(await _contractorService.GetContractorAsync(request, cancellationToken));
        }

        [HttpPost("GetContractors")]
        [Authorize]
        public async Task<ActionResult<GetContractorResponse>> GetContractors(PaginationRequest<GetFilteredContractorsRequest, GetFilteredContractorsResponse> request, CancellationToken cancellationToken)
        {
            return Ok(await _contractorService.GetPagedFilteresContractors(request, cancellationToken));
        }

        [HttpPost("UpdateContractor")]
        [Authorize]
        public async Task<ActionResult> UpdateContractor(UpdateContractorDto request, CancellationToken cancellationToken)
        {
            await _contractorService.UpdateContractorAsync(request, cancellationToken);
            return Ok();

        }

        [HttpPost("DeleteContractor")]
        [Authorize]
        public async Task<ActionResult> DeleteContractor(DeleteContractorRequest request, CancellationToken cancellationToken)
        {
            await _contractorService.DeleteContractorAsync(request, cancellationToken);
            return Ok();
        }
    }
}
