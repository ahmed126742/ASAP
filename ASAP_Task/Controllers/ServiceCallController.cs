using ASAP.Application.Common.Models;
using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Fitting.DTOs.Processing;
using ASAP.Application.Services.User.Fitting.DTOs.Retrieval;
using ASAP.Application.Services.User.ServiceCall.DTOs;
using ASAP.Application.Services.User.ServiceCall.DTOs.Processing;
using ASAP.Application.Services.User.ServiceCall.DTOs.Retireval;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCallController : ControllerBase
    {
        public ServiceCallController()
        {
            
        }

        [HttpPost("CreateServiceCall")]
        public async Task<ActionResult<Guid>> CreateServiceCall(CreateServiceCallRequest request, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("CompleteMyServiceCall")]
        public async Task<ActionResult> CompleteMyServiceCall(CompleteMyServiceCallRequest request, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("GetMyJobs")]
        public async Task<ActionResult<PagedReponse<GetUserJobsResponse>>> GetMyJobs(PaginationRequest<GetUserJobsRequest, GetUserJobsResponse> request, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("GetServiceCall")]
        public async Task<ActionResult<GetServiceCallResponse>> GetServiceCall(ServiceCallIdentityDto request, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("Admin/GetServiceCalls")]
        public async Task<ActionResult<PagedReponse<GetServiceCallResponse>>> GetServiceCalls(PaginationRequest<GetServicesCallsRequest, GetServiceCallResponse> request, CancellationToken cancellationToken)
        {
            return Ok();
        }


        [HttpPost("GetMyServiceCalls")]
        public async Task<ActionResult<List<GetUserJobsResponse>>> GetMyServiceCalls(GetUserJobsRequest request, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("UpdateServiceCall")]
        public async Task<ActionResult> UpdateServiceCall(UpdateServiceCallRequest request, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("DeleteServiceCall")]
        public async Task<ActionResult> DeleteServiceCall(ServiceCallIdentityDto request, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
