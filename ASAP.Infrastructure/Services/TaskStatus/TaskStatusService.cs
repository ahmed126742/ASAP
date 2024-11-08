using ASAP.Application.Common.Enums;
using ASAP.Application.Services.TaskStatus;
using ASAP.Application.Services.TaskStatus.DTOs.Processing;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;

namespace ASAP.Infrastructure.Services.TaskStatus
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly IContractItemRepository _contractItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskStatusService(
            IContractItemRepository contractItemRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _contractItemRepository = contractItemRepository;
        }

        public async Task TakeWorkflowActionRequest(TakeWorkflowActionRequest request, CancellationToken cancellationToken)
        {
            var contractItem = await _contractItemRepository.Get(request.ContractItemId, cancellationToken);
            if (contractItem == null)
                throw new Exception("contract item does not exist!");

            contractItem.Status = request.JobStatusId != null ? (int)request.JobStatusId : + contractItem.Status ;
            _contractItemRepository.Update(contractItem);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task UpdateContractItemStatusToInstalled(Guid contractItemId, CancellationToken cancellationToken)
        {
            var contractItem = await _contractItemRepository.Get(contractItemId, cancellationToken);

            if (contractItem == null)
                throw new Exception("contract item not exist!");

            contractItem.Status = (int)JobStatusEnum.Installed;
            _contractItemRepository.Update(contractItem);
        }

        public async Task UpdateContractItemStatusToRemarked(Guid contractItemId, CancellationToken cancellationToken)
        {
            var contractItem = await _contractItemRepository.Get(contractItemId, cancellationToken);

            if (contractItem == null)
                throw new Exception("contract item not exist!");

            contractItem.Status = (int)JobStatusEnum.Remake;
            _contractItemRepository.Update(contractItem);
        }
    }
}
