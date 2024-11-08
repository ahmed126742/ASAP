using ASAP.Application.Common.Enums;

namespace ASAP.Application.Services.TaskStatus.DTOs.Processing
{
    public class TakeWorkflowActionRequest
    {
        public Guid ContractItemId { get; set; }

        public JobStatusEnum JobStatusId { get; set; }
    }
}
