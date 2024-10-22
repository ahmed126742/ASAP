using ASAP.Application.Common.Enums;
using ASAP.Application.Common.Models;
using ASAP.Application.Services.ContractItems;
using ASAP.Application.Services.ContractItems.DTOs;
using ASAP.Application.Services.ContractItems.DTOs.Processing;
using ASAP.Application.Services.ContractItems.DTOs.Retrieval;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Infrastructure.Services.Contract
{
    public class ContractItemService : IContractItemsService
    {
        private readonly IContractItemRepository _contractItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ContractItemService(
            IContractItemRepository contractItemRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _contractItemRepository = contractItemRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateContractItem(CreateContractItemRequest request, CancellationToken cancellationToken)
        {
            if (request.FitterId != null)
            {
                var fitter = await _userRepository.Get(request.FitterId.Value, cancellationToken);
                if (fitter == null)
                    throw new Exception("fitter does not exist!");
            }

            if (request.SurveyorId != null)
            {
                var surveyor = await _userRepository.Get(request.SurveyorId.Value, cancellationToken);
                if (surveyor == null)
                    throw new Exception("surveyor does not exist!");
            }

            
            var contractItem = _mapper.Map<ContractItem>(request);
            contractItem.Status = (int)JobStatusEnum.ToSurvey;
            _contractItemRepository.Create(contractItem);
            await _unitOfWork.Save(cancellationToken);
            return contractItem.Id;
        }

        public async Task DeleteContractItem(ContractItemIdentity request, CancellationToken cancellationToken)
        {
            var contractItem = await _contractItemRepository.Get(request.Id, cancellationToken);
            if (contractItem == null)
                throw new Exception("Production Id does not exist");

            _contractItemRepository.Delete(contractItem);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<GetContractItemResponse> GetContractItem(ContractItemIdentity request, CancellationToken cancellationToken)
        {
            var contractItem = await _contractItemRepository.Get(request.Id, cancellationToken);
            if (contractItem == null)
                throw new Exception("Production Id does not exist");

            return _mapper.Map<GetContractItemResponse>(contractItem);
        }

        public async Task<PagedReponse<GetFilteredContractItemReponse>> GetContractItemFiltered(PaginationRequest<GetFilteredContractItemRequest, GetFilteredContractItemReponse> request, CancellationToken cancellationToken)
        {
            var contractItems = _contractItemRepository.GetFilteredContractItems(request.PageNumber, request.PageNumber, request.Filters.ContractId, request.Filters.InstallationDateFrom, request.Filters.InstallationDateTo);
            var filteredContractItems = _mapper.Map<List<GetFilteredContractItemReponse>>(await contractItems.ToListAsync(cancellationToken));
            return new PagedReponse<GetFilteredContractItemReponse>(filteredContractItems, await contractItems.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
        }

        public async Task UpdateContractItem(UpdateContractItemRequest request, CancellationToken cancellationToken)
        {
            var contractItem = await _contractItemRepository.Get(request.Id, cancellationToken);
            if (contractItem == null)
                throw new Exception("Production Id does not exist");

            _mapper.Map(request, contractItem);
            _contractItemRepository.Update(contractItem);
            await _unitOfWork.Save(cancellationToken);
        }
    }
}
