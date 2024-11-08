using ASAP.Application.Common.Enums;
using ASAP.Application.Common.Models;
using ASAP.Application.Services.ContractItems.DTOs;
using ASAP.Application.Services.TaskStatus;
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
        private readonly IServiceCallRepository _serviceCallRepository;
        private readonly IContractItemRepository _contractItemRepository;
        private readonly ITaskStatusService _taskStatusService;
        private readonly IMapper _mapper;

        public FitterService(
            IUnitOfWork unitOfWork,
            IFittingRepository fittingRepository,
            IContractItemRepository contractItemRepository,
            IServiceCallRepository serviceCallRepository,
            ITaskStatusService taskStatusService,
            IMapper mapper)
        {
            _fittingRepository = fittingRepository;
            _serviceCallRepository = serviceCallRepository;
            _contractItemRepository = contractItemRepository;
            _taskStatusService = taskStatusService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateFitting(CreateFittingRequest request, CancellationToken cancellationToken)
        {
            await _taskStatusService.UpdateContractItemStatusToInstalled(request.ContractItemId, cancellationToken);
            var fittingJob = _mapper.Map<Fitting>(request);
            _fittingRepository.Create(fittingJob);
            await _unitOfWork.Save(cancellationToken);
            return fittingJob.Id;
        }

        public async Task<Guid> CreateOutstandingFitting(CreateOutstandingFittingRequest request, CancellationToken cancellationToken)
        {
            await _taskStatusService.UpdateContractItemStatusToRemarked(request.ContractItemId, cancellationToken);
            var fittingJob = _mapper.Map<Fitting>(request);
            _fittingRepository.Create(fittingJob);
            _serviceCallRepository.Create(new Domain.Entities.ServiceCall { ReportedIssue = request.ReportedIssue, ContractItemId = request.ContractItemId});
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
            var fittings = _fittingRepository.GetAllAsQuarble();
            var pagedFittingss = fittings.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => _mapper.Map<GetFittingResponse>(x));

            return new PagedReponse<GetFittingResponse>(pagedFittingss, await fittings.CountAsync(), request.PageNumber, request.PageSize);;
        }

        public async Task<GetFittingResponse> GetFittingByContractItem(ContractItemIdentity request, CancellationToken cancellationToken)
        {
            var surveys = await _fittingRepository.GetAllAsQuarble(x => x.ContractItemId == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (surveys == null)
                throw new Exception("contract Item does not exist!");

            return _mapper.Map<GetFittingResponse>(surveys);
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
            var filteredContractItems = _contractItemRepository.GetAllAsQuarble(x => x.FitterId == request.Filters.UserId && x.Status == (int)JobStatusEnum.Booked)
                                .Include(c => c.Contract)
                                .Select(x => _mapper.Map<GetUserJobsResponse>(x));

            return new PagedReponse<GetUserJobsResponse>(filteredContractItems, await filteredContractItems.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
        }
    }
}
