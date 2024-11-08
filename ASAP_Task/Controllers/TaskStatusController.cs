using ASAP.Application.Services.TaskStatus;
using ASAP.Application.Services.TaskStatus.DTOs.Processing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITaskStatusService _taskStatusService;
        public TaskStatusController(ITaskStatusService taskStatusService)
        {
            _taskStatusService = taskStatusService;
        }

        [HttpPost("TakeWorkflowAction")]
        public async Task<ActionResult> TakeWorkflowActionRequest(TakeWorkflowActionRequest request, CancellationToken cancellationToken )
        {
            await _taskStatusService.TakeWorkflowActionRequest(request, cancellationToken);
            return Ok();
        }
    }
}
