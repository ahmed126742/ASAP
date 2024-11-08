using ASAP.Application.Common.Models;
using ASAP.Application.Services.Contract.DTOs;
using ASAP.Application.Services.ContractItems;
using ASAP.Application.Services.ContractItems.DTOs;
using ASAP.Application.Services.ContractItems.DTOs.Processing;
using ASAP.Application.Services.ContractItems.DTOs.Retrieval;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractItemController : ControllerBase
    {
        private readonly IContractItemsService _contractItemService;

        public ContractItemController(IContractItemsService contractItemsService)
        {
            _contractItemService = contractItemsService;
        }

        [HttpPost("CreateContractItems")]
        [Authorize]
        public async Task<ActionResult<Guid>> CreateContractItems(CreateContractItemRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("request can not be null or empty");

            return Ok(await _contractItemService.CreateContractItem(request, cancellationToken));
        }

        [HttpPost("GetContractItem")]
        [Authorize]
        public async Task<ActionResult<GetContractItemResponse>> GetContract(ContractItemIdentity request, CancellationToken cancellationToken)
        {
            return Ok(await _contractItemService.GetContractItem(request, cancellationToken));
        }

        [HttpPost("GetContractItemCounts")]
        [Authorize]
        public async Task<ActionResult<GetContractItemCountResponse>> GetContractItemCounts(ContractIdentityDto request, CancellationToken cancellationToken)
        {
            return Ok(await _contractItemService.GetContractItemsCounts(request, cancellationToken));
        }

        [HttpPost("GetContractItems")]
        [Authorize]
        public async Task<ActionResult<PagedReponse<GetFilteredContractItemReponse>>> GetContracts(PaginationRequest<GetFilteredContractItemRequest, GetFilteredContractItemReponse> request, CancellationToken cancellationToken)
        {
            return Ok(await _contractItemService.GetContractItemFiltered(request, cancellationToken));
        }

        [HttpPost("UpdateContract")]
        [Authorize]
        public async Task<ActionResult> UpdateContract(UpdateContractItemRequest request, CancellationToken cancellationToken)
        {
            await _contractItemService.UpdateContractItem(request, cancellationToken);
            return Ok();
        }

        [HttpPost("DeleteContractItem")]
        [Authorize]
        public async Task<ActionResult> DeleteContract(ContractItemIdentity request, CancellationToken cancellationToken)
        {
            await _contractItemService.DeleteContractItem(request, cancellationToken);
            return Ok();
        }
    }
}
