using ASAP.Application.Services.TaskStatus.DTOs.Processing;

namespace ASAP.Application.Services.TaskStatus
{
    public interface ITaskStatusService
    {
        Task TakeWorkflowActionRequest(TakeWorkflowActionRequest request, CancellationToken cancellationToken);

        Task UpdateContractItemStatusToInstalled(Guid contractItemId, CancellationToken cancellationToken);

        Task UpdateContractItemStatusToRemarked(Guid contractItemId, CancellationToken cancellationToken);
    }
}
