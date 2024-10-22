using ASAP.Application.Common.Models;
using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Fitting;
using ASAP.Application.Services.User.Fitting.DTOs.Processing;
using ASAP.Application.Services.User.Fitting.DTOs.Retrieval;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitterController : ControllerBase
    {
        private readonly IFitterService _fitterService;
        public FitterController(IFitterService fitterService)
        {
            _fitterService = fitterService;
        }

        [HttpPost("CreateFitter")]
        public async Task<ActionResult<Guid>> CreateFitter(CreateFittingRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _fitterService.CreateFitting(request, cancellationToken));
        }

        [HttpPost("GetMyJobs")]
        public async Task<ActionResult<PagedReponse<GetUserJobsResponse>>> GetMyJobs(PaginationRequest<GetUserJobsRequest, GetUserJobsResponse> request, CancellationToken cancellationToken)
        {
            return Ok(await _fitterService.GetMyJobs(request, cancellationToken));
        }


        [HttpPost("GetFitter")]
        public async Task<ActionResult<GetFittingResponse>> GetFitter(GetFittingRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _fitterService.GetFitting(request, cancellationToken));
        }

        [HttpPost("Admin/GetFitters")]
        public async Task<ActionResult<PagedReponse<GetFittingResponse>>> GetFitters(PaginationRequest<GetFittingsRequest, GetFittingResponse> request, CancellationToken cancellationToken)
        {
            var result = await _fitterService.GetFittings(request, cancellationToken);
            return Ok(result);
        }


        [HttpPost("GetMyFittings")]
        public async Task<ActionResult<IList<GetUserJobsResponse>>> GetMyFittings(GetUserJobsRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _fitterService.GetMyFittings(request, cancellationToken));
        }

        [HttpPost("UpdateFitter")]
        public async Task<ActionResult> UpdateFitter(UpdateFittingRequest request, CancellationToken cancellationToken)
        {
            await _fitterService.UpdateFitting(request, cancellationToken);
            return Ok();
        }

        [HttpPost("DeleteFitter")]
        public async Task<ActionResult> DeleteFitter(DeleteFittingRequest request, CancellationToken cancellationToken)
        {
            await _fitterService.DeleteFitting(request, cancellationToken);
            return Ok();
        }
    }
}
