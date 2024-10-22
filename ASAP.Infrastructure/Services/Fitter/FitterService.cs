using ASAP.Application.Common.Enums;
using ASAP.Application.Common.Models;
using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Fitting;
using ASAP.Application.Services.User.Fitting.DTOs.Processing;
using ASAP.Application.Services.User.Fitting.DTOs.Retrieval;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Infrastructure.Services.Fitter
{
    public class FitterService : IFitterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFittingRepository _fittingRepository;
        private readonly IContractItemRepository _contractItemRepository;
        private readonly IMapper _mapper;

        public FitterService(
            IUnitOfWork unitOfWork,
            IFittingRepository fittingRepository,
            IContractItemRepository contractItemRepository,
            IMapper mapper)
        {
            _fittingRepository = fittingRepository;
            _contractItemRepository = contractItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> CreateFitting(CreateFittingRequest request, CancellationToken cancellationToken)
        {
            var fittingJob = _mapper.Map<Fitting>(request);
            _fittingRepository.Create(fittingJob);
            await _unitOfWork.Save(cancellationToken);
            return fittingJob.Id;
        }

        public async Task<GetFittingResponse> GetFitting(GetFittingRequest request, CancellationToken cancellationToken)
        {
            var fitting = await _fittingRepository.Get(request.Id, cancellationToken);
            if (fitting == null)
                throw new Exception("fitting job does not exist!");

            return _mapper.Map<GetFittingResponse>(fitting);
        }

        public async Task<PagedReponse<GetFittingResponse>> GetFittings(PaginationRequest<GetFittingsRequest, GetFittingResponse> request, CancellationToken cancellationToken)
        {
            var fittings = await _fittingRepository.GetAllAsQuarble().ToListAsync(cancellationToken);
            if (!fittings.Any())
                throw new Exception("contract Item does not exist!");

            return _mapper.Map<PagedReponse<GetFittingResponse>>(fittings);
        }

        public async Task<IList<GetUserJobsResponse>> GetMyFittings(GetUserJobsRequest request, CancellationToken cancellationToken)
        {
            var fitterId = Guid.NewGuid();
            var myFittingsjob = await _fittingRepository.GetAllAsQuarble(
                    x => x.ContractItem != null 
                    && x.ContractItem.FitterId == fitterId
                    && x.Deleted.Value)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<GetUserJobsResponse>>(myFittingsjob);
        }

        public async Task UpdateFitting(UpdateFittingRequest request, CancellationToken cancellationToken)
        {
            var fitting = await _fittingRepository.Get(request.Id, cancellationToken);
            if (fitting == null)
                throw new Exception("fitting job does not exist!");

            _mapper.Map(request, fitting);
            _fittingRepository.Update(fitting);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task DeleteFitting(DeleteFittingRequest request, CancellationToken cancellationToken)
        {
            var fitting = await _fittingRepository.Get(request.Id, cancellationToken);
            if (fitting == null)
                throw new Exception("fitting job does not exist!");

            _fittingRepository.Delete(fitting);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<PagedReponse<GetUserJobsResponse>> GetMyJobs(PaginationRequest<GetUserJobsRequest, GetUserJobsResponse> request, CancellationToken cancellationToken)
        {
            var contractItem = _contractItemRepository.GetAllAsQuarble(x => x.FitterId == Guid.NewGuid() && x.Status == (int)JobStatusEnum.Booked);
            var filteredContractItems = _mapper.Map<List<GetUserJobsResponse>>(await contractItem.ToListAsync(cancellationToken));
            return new PagedReponse<GetUserJobsResponse>(filteredContractItems, await contractItem.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
        }
    }
}
