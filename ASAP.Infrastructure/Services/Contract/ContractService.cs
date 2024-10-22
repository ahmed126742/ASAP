using System.Threading;
using ASAP.Application.Common;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ContractService(
            IContractRepository contractRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _contractRepository = contractRepository;
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

            var contracts = _contractRepository.GetFilteredContracts(request.PageNumber, request.PageSize, request.Filters.SearchText, (int)request.Filters.ContractTypeId, request.Filters.Address);
            var filteredContracts = _mapper.Map<List<GetFilteredContractsResponse>>(await contracts.ToListAsync(cancellationToken));
            return new PagedReponse<GetFilteredContractsResponse>(filteredContracts, await contracts.CountAsync(), request.PageNumber, request.PageSize);
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
