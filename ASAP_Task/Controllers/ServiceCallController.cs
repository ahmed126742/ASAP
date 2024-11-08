using ASAP.Application.Common.Models;
using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Fitting.DTOs.Processing;
using ASAP.Application.Services.User.Fitting.DTOs.Retrieval;
using ASAP.Application.Services.User.ServiceCall;
using ASAP.Application.Services.User.ServiceCall.DTOs;
using ASAP.Application.Services.User.ServiceCall.DTOs.Processing;
using ASAP.Application.Services.User.ServiceCall.DTOs.Retireval;
using ASAP.Application.Services.User.Survey.DTOs.Processing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ServiceCallController : ControllerBase
    {
        private readonly IServiceCallService _serviceCall;
        public ServiceCallController(IServiceCallService serviceCall)
        {
            _serviceCall = serviceCall;
        }

        [HttpPost("CreateServiceCall")]
        public async Task<ActionResult<Guid>> CreateServiceCall(CreateServiceCallRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _serviceCall.CreateServiceCall(request, cancellationToken));
        }

        [HttpPost("RevisitServiceCall")]
        public async Task<ActionResult> RevisitServiceCall(ServiceCallIdentityDto request, CancellationToken cancellationToken)
        {
            await _serviceCall.RevisitServiceCall(request, cancellationToken);
            return Ok();
        }

        [HttpPost("CompleteMyServiceCall")]
        public async Task<ActionResult> CompleteMyServiceCall(CompleteMyServiceCallRequest request, CancellationToken cancellationToken)
        {
            await _serviceCall.CompleteServiceCall(request, cancellationToken);
            return Ok();
        }

        [HttpPost("GetMyJobs")]
        public async Task<ActionResult<PagedReponse<GetServiceCallJobsResponse>>> GetMyJobs(PaginationRequest<GetUserJobsRequest, GetServiceCallJobsResponse> request, CancellationToken cancellationToken)
        {
             return Ok(await _serviceCall.GetMyJobs(request, cancellationToken));
        }

        [HttpPost("GetServiceCallByContractItem")]
        public async Task<ActionResult<GetServiceCallResponse>> GetServiceCallByContractItem(ServiceCallIdentityDto request, CancellationToken cancellationToken)
        {
             return Ok(await _serviceCall.GetServiceCallByContractItem(request, cancellationToken));
        }

        [HttpPost("GetServiceCall")]
        public async Task<ActionResult<GetServiceCallResponse>> GetServiceCall(ServiceCallIdentityDto request, CancellationToken cancellationToken)
        {
            return Ok(await _serviceCall.GetServiceCall(request, cancellationToken));
        }

        [HttpPost("Admin/GetServiceCalls")]
        public async Task<ActionResult<PagedReponse<GetServiceCallResponse>>> GetServiceCalls(PaginationRequest<GetServicesCallsRequest, GetServiceCallResponse> request, CancellationToken cancellationToken)
        {
            return Ok(await _serviceCall.GetServiceCalls(request, cancellationToken));
        }

        [HttpPost("UpdateServiceCall")]
        public async Task<ActionResult> UpdateServiceCall(UpdateServiceCallRequest request, CancellationToken cancellationToken)
        {
            await _serviceCall.UpdateServiceCall(request, cancellationToken);
            return Ok();
        }

        [HttpPost("DeleteServiceCall")]
        public async Task<ActionResult> DeleteServiceCall(DeleteSurveyRequest request, CancellationToken cancellationToken)
        {
            await _serviceCall.DeleteServiceCall(request, cancellationToken);
            return Ok();
        }
    }
}
