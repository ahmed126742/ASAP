using ASAP.Application.Common;
using ASAP.Application.Common.Models;
using ASAP.Application.Services.Contractor;
using ASAP.Application.Services.Contractor.DTOs.Processing;
using ASAP.Application.Services.Contractor.DTOs.Retreival;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Infrastructure.Services.Contract
{
    public class ContractorService : IContractorService
    {
        public readonly IContractorRepository _contractorRepository;
        public readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ContractorService(
            IContractorRepository contractorRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _contractorRepository = contractorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateContractor(CreateContractorDto request, CancellationToken cancellationToken)
        {
            var contractor = _mapper.Map<Domain.Entities.Contractor>(request);
            _contractorRepository.Create(contractor);
            await _unitOfWork.Save(cancellationToken);
            return contractor.Id;
        }

        public async Task DeleteContractorAsync(DeleteContractorRequest request, CancellationToken cancellationToken)
        {
            var contractor = await _contractorRepository.Get(request.Id, cancellationToken);
            if (contractor == null)
                throw new NotFoundException("Contractor Id deos not exist!");

            _contractorRepository.Delete(contractor);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<GetContractorResponse> GetContractorAsync(GetContractorRequest request, CancellationToken cancellationToken)
        {
            var supplier = await _contractorRepository.Get(request.Id, cancellationToken);
            if (supplier == null)
                throw new NotFoundException("Contractor deos not exist!");

            return _mapper.Map<GetContractorResponse>(supplier);
        }

        public async Task<PagedReponse<GetFilteredContractorsResponse>> GetPagedFilteresContractors(PaginationRequest<GetFilteredContractorsRequest, GetFilteredContractorsResponse> request, CancellationToken cancellationtoken)
        {
            var contractors = _contractorRepository.GetFilteredContractors(request.Filters.SearchText);
            var filteredContractors = _mapper.Map<List<GetFilteredContractorsResponse>>(await contractors.ToListAsync(cancellationtoken));
            return new PagedReponse<GetFilteredContractorsResponse>(filteredContractors, await contractors.CountAsync(), request.PageNumber, request.PageSize);
        }

        public async Task UpdateContractorAsync(UpdateContractorDto request, CancellationToken cancellationToken)
        {
            var supplier = await _contractorRepository.Get(request.Id, cancellationToken);
            if (supplier == null)
                throw new NotFoundException("SupplierId does not exist!");

            _mapper.Map(request, supplier);
            _contractorRepository.Update(supplier);
            await _unitOfWork.Save(cancellationToken);
        }
    }
}
