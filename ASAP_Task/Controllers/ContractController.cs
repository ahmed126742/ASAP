using ASAP.Application.Common.Models;
using ASAP.Application.Services.Contract;
using ASAP.Application.Services.Contract.DTOs.Processing;
using ASAP.Application.Services.Contract.DTOs.Retreival;
using ASAP.Application.Services.Contractor.DTOs.Retreival;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpPost("CreateContract")]
        [Authorize]
        public async Task<ActionResult<Guid>> CreateContract(CreateContractRequest request,CancellationToken cancellationToken)
        {
            return Ok(await _contractService.CreateContract(request, cancellationToken));
        }

        [HttpPost("ArchiveContracts")]
        [Authorize]
        public async Task<ActionResult> ArchiveContracts(ArchiveContractRequest request, CancellationToken cancellationToken)
        {
            await _contractService.ArchiveContracts(request, cancellationToken);
            return Ok();
        }

        [HttpPost("GetContract")]
        [Authorize]
        public async Task<ActionResult<GetContractResponse>> GetContract(GetContractRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _contractService.GetContractAsync(request, cancellationToken));
        }

        [HttpPost("GetContracts")]
        [Authorize]
        public async Task<ActionResult<PagedReponse<GetFilteredContractsResponse>>> GetContracts(PaginationRequest<GetFilteredContractsRequest, GetFilteredContractsResponse> request, CancellationToken cancellationToken)
        {
            return Ok(await _contractService.GetPagedFilteresContracts(request, cancellationToken));
        }

        [HttpPost("UpdateContract")]
        [Authorize]
        public async Task<ActionResult> UpdateContract(UpdateContractRequest request, CancellationToken cancellationToken)
        {
            await _contractService.UpdateContractAsync(request,cancellationToken);
            return Ok();
        }

        [HttpPost("DeleteContract")]
        [Authorize]
        public async Task<ActionResult> DeleteContract(DeleteContractRequest request, CancellationToken cancellationToken)
        {
            await _contractService.DeleteContractAsync(request, cancellationToken);
            return Ok();
        }
    }
}
