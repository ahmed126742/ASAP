using System.Threading;
using ASAP.Application.Common;
using ASAP.Application.Common.Enums;
using ASAP.Application.Common.Models;
using ASAP.Application.Services.Contract;
using ASAP.Application.Services.Contract.DTOs.Processing;
using ASAP.Application.Services.Contract.DTOs.Retreival;
using ASAP.Application.Services.Contractor.DTOs.Processing;
using ASAP.Application.Services.Contractor.DTOs.Retreival;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Infrastructure.Services.Contract
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IContractItemRepository _contractItemRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ContractService(
            IContractRepository contractRepository,
            IContractItemRepository contractItemRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _contractRepository = contractRepository;
            _contractItemRepository = contractItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> CreateContract(CreateContractRequest request, CancellationToken cancellationToken)
        {
            var contract = _mapper.Map<Domain.Entities.Contract>(request);
            _contractRepository.Create(contract);
            await _unitOfWork.Save(cancellationToken);
            return contract.Id;
        } 
        
        public async Task ArchiveContracts(ArchiveContractRequest request, CancellationToken cancellationToken)
        {
            var contract = await _contractRepository.Get(request.Id, cancellationToken);
            if (contract == null)
                throw new Exception("contract does not exist!");

            contract.IsArchived = request.IsArchived;
            _contractRepository.Update(contract);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task DeleteContractAsync(DeleteContractRequest request, CancellationToken cancellationToken)
        {
            var contract = await _contractRepository.Get(request.Id, cancellationToken);
            if (contract == null)
                throw new NotFoundException("Contract Id deos not exist!");

            _contractRepository.Delete(contract);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<GetContractResponse> GetContractAsync(GetContractRequest request, CancellationToken cancellationToken)
        {
            var contract = await _contractRepository.Get(request.Id, cancellationToken);
            if (contract == null)
                throw new NotFoundException("Contract deos not exist!");

            return _mapper.Map<GetContractResponse>(contract);
        }

        public async Task<PagedReponse<GetFilteredContractsResponse>> GetPagedFilteresContracts(PaginationRequest<GetFilteredContractsRequest, GetFilteredContractsResponse> request, CancellationToken cancellationToken)
        {
            var filteredContractsQuarabl = await _contractRepository.GetFilteredContracts(request.PageNumber, request.PageSize, request.Filters.SearchText, (int)request.Filters.ContractTypeId, request.Filters.IsArchived, request.Filters.Address)
                .Select(x => _mapper.Map<GetFilteredContractsResponse>(x))
                .ToListAsync(cancellationToken) ;

            var contractItems = _contractItemRepository.GetAllAsQuarble(x => filteredContractsQuarabl.Select(s => s.Id).Contains(x.ContractId));
            foreach (var contract in filteredContractsQuarabl)
            {
                contract.ContractItemsCount = await contractItems.CountAsync(x => x.ContractId == contract.Id, cancellationToken);
                contract.CompletionCount = await contractItems
                    .Where( x => x.ContractId == contract.Id  && x.Status.HasValue  && x.Status.Value == (int)JobStatusEnum.Complete)
                    .CountAsync(cancellationToken);

                contract.Status = contract.ContractItemsCount == 0 ?
                    "Not Started" : contract.ContractItemsCount - contract.CompletionCount == 0 ?
                    "Completed" : contract.ContractItemsCount - contract.CompletionCount == contract.ContractItemsCount ?
                    "Not Started" : "Pending" ; 
            }
            return new PagedReponse<GetFilteredContractsResponse>(filteredContractsQuarabl.AsQueryable(), _contractRepository.GetAllAsQuarble().Count(), request.PageNumber, request.PageSize);
        }

        public async Task UpdateContractAsync(UpdateContractRequest request, CancellationToken cancellationToken)
        {
            var contract = await _contractRepository.Get(request.Id, cancellationToken);
            if (contract == null)
                throw new NotFoundException("Contract Id does not exist!");

            _mapper.Map(request, contract);
            _contractRepository.Update(contract);
            await _unitOfWork.Save(cancellationToken);
        }
    }
}
