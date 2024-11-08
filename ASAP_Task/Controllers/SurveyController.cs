using ASAP.Application.Common.Models;
using ASAP.Application.Services.ContractItems.DTOs;
using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Survey;
using ASAP.Application.Services.User.Survey.DTOs.Processing;
using ASAP.Application.Services.User.Survey.DTOs.Retrieval;
using ASAP_Task.Authentication.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }


        [HttpPost("GetMyJobs")]
        public async Task<ActionResult<PagedReponse<GetUserJobsResponse>>> GetMyJobs(PaginationRequest<GetUserJobsRequest, GetUserJobsResponse> request, CancellationToken cancellationToken)
        {
            return Ok(await _surveyService.GetMyJobs(request, cancellationToken));
        }

        [HttpPost("CreateSurvey")]
        public async Task<ActionResult> CreateSurvey(CreateSurveyRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _surveyService.CreateSurvey(request, cancellationToken));
        }


        [HttpPost("GetSurvey")]
        public async Task<ActionResult<GetSurveyResponse>> GetSurvey(GetSurveyRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _surveyService.GetSurvey(request, cancellationToken));
        }

        [HttpPost("GetSurveys")]
        public async Task<ActionResult<PagedReponse<GetSurveyResponse>>> GetSurveys(PaginationRequest<GetSurverysRequest,GetSurveyResponse> request, CancellationToken cancellationToken)
        {
            return Ok(await _surveyService.GetSurveys(request, cancellationToken));
        }

        [HttpPost("GetSurveysByContractItem")]
        public async Task<ActionResult<IList<GetSurveyResponse>>> GetSurveysByContractItem(ContractItemIdentity request, CancellationToken cancellationToken)
        {
            return Ok(await _surveyService.GetSurveysByContractItem(request, cancellationToken));
        }

        [HttpPost("UpdateSurvey")]
        public async Task<ActionResult> UpdateSurvey(UpdateSurveyRequest request, CancellationToken cancellationToken)
        {
            await _surveyService.UpdateSurvey(request, cancellationToken);
            return Ok();

        }

        [HttpPost("DeleteSurvey")]
        public async Task<ActionResult> DeleteSurvey(DeleteSurveyRequest request, CancellationToken cancellationToken)
        {
            await _surveyService.DeleteSurvey(request, cancellationToken);
            return Ok();
        }
    }
}
