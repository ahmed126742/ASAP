using ASAP.Application.Common.Enums;
using ASAP.Application.Common.Models;
using ASAP.Application.Services.TaskStatus;
using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.ServiceCall;
using ASAP.Application.Services.User.ServiceCall.DTOs;
using ASAP.Application.Services.User.ServiceCall.DTOs.Processing;
using ASAP.Application.Services.User.ServiceCall.DTOs.Retireval;
using ASAP.Application.Services.User.Survey.DTOs.Processing;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using ASAP.Infrastructure.Services.TaskStatus;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Infrastructure.Services.ServiceCall
{
    public class ServiceCallService : IServiceCallService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceCallRepository _serviceCallRepository;
        private readonly IContractItemRepository _contractItemRepository;
        private readonly ITaskStatusService _taskStatusService;
        private readonly IMapper _mapper;

        public ServiceCallService(
            IUnitOfWork unitOfWork,
            IServiceCallRepository serviceCallRepository,
            IContractItemRepository contractItemRepository,
            ITaskStatusService taskStatusService,
            IMapper mapper)
        {
            _serviceCallRepository = serviceCallRepository;
            _contractItemRepository = contractItemRepository;
            _mapper = mapper;
            _taskStatusService = taskStatusService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateServiceCall(CreateServiceCallRequest request, CancellationToken cancellationToken)
        {
            var serviceCall = _mapper.Map<Domain.Entities.ServiceCall>(request);
            _serviceCallRepository.Create(serviceCall);
            await _unitOfWork.Save(cancellationToken);
            return serviceCall.Id;
        }

        public async Task<GetServiceCallResponse> GetServiceCall(ServiceCallIdentityDto request, CancellationToken cancellationToken)
        {
            var serviceCall = await _serviceCallRepository.Get(request.Id.Value, cancellationToken);
            if (serviceCall == null)
                throw new Exception("Service call job does not exist!");

            return _mapper.Map<GetServiceCallResponse>(serviceCall);
        }

        public async Task<GetServiceCallResponse> GetServiceCallByContractItem(ServiceCallIdentityDto request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue)
                throw new Exception("Id of contract item must be provided!");

            var serviceCall = await _serviceCallRepository.GetServiceCallByContractItemAsync(request.Id.Value, cancellationToken);
            if (serviceCall == null)
                throw new Exception("Service call job does not exist!");

            return _mapper.Map<GetServiceCallResponse>(serviceCall);
        }


        public async Task RevisitServiceCall(ServiceCallIdentityDto request, CancellationToken cancellationToken)
        {
            var serviceCall = await _serviceCallRepository.Get(request.Id.Value, cancellationToken);
            if (serviceCall == null)
                throw new Exception("Service call job does not exist!");

            await _taskStatusService.UpdateContractItemStatusToRemarked(serviceCall.ContractItemId.GetValueOrDefault(), cancellationToken);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task CompleteServiceCall(CompleteMyServiceCallRequest request, CancellationToken cancellationToken)
        {
            var serviceCall = await _serviceCallRepository.Get(request.Id.Value, cancellationToken);
            if (serviceCall == null)
                throw new Exception("Service call job does not exist!");

            _mapper.Map(request, serviceCall);
            _serviceCallRepository.Update(serviceCall);
            await _taskStatusService.UpdateContractItemStatusToInstalled(serviceCall.ContractItemId.GetValueOrDefault(), cancellationToken);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<PagedReponse<GetServiceCallResponse>> GetServiceCalls(PaginationRequest<GetServicesCallsRequest, GetServiceCallResponse> request, CancellationToken cancellationToken)
        {
            var serviceCalls = _serviceCallRepository.GetAllAsQuarble();

            var pagedServiceCalls = serviceCalls.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => _mapper.Map<GetServiceCallResponse>(x));

            return new PagedReponse<GetServiceCallResponse>(pagedServiceCalls,await serviceCalls.CountAsync(), request.PageNumber, request.PageSize);
        }

        public async Task<IList<GetUserJobsResponse>> GetMyServiceCalls(GetUserJobsRequest request, CancellationToken cancellationToken)
        {
            var engineerId = Guid.NewGuid();
            var myServiceCall = await _serviceCallRepository.GetAllAsQuarble(
                    x => x.ContractItem != null
                    && x.UserId == engineerId
                    && x.Deleted.Value)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<GetUserJobsResponse>>(myServiceCall);
        }

        public async Task UpdateServiceCall(UpdateServiceCallRequest request, CancellationToken cancellationToken)
        {
            var serviceCall = await _serviceCallRepository.Get(request.Id, cancellationToken);
            if (serviceCall == null)
                throw new Exception("Service call job does not exist!");

            _mapper.Map(request, serviceCall);
            _serviceCallRepository.Update(serviceCall);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task DeleteServiceCall(DeleteSurveyRequest request, CancellationToken cancellationToken)
        {
            var serviceCall = await _serviceCallRepository.Get(request.Id.Value, cancellationToken);
            if (serviceCall == null)
                throw new Exception("Service call job does not exist!");

            _serviceCallRepository.Delete(serviceCall);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<PagedReponse<GetUserJobsResponse>> GetMyServiceCalls(PaginationRequest<GetUserJobsRequest, GetUserJobsResponse> request, CancellationToken cancellationToken)
        {
            var filteredContractItems = _contractItemRepository.GetAllAsQuarble(x => x.FitterId == Guid.NewGuid() && x.Status == (int)JobStatusEnum.Booked)
                .Select(x => _mapper.Map<GetUserJobsResponse>(x));

            return new PagedReponse<GetUserJobsResponse>(filteredContractItems, await filteredContractItems.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
        }

        public async Task<PagedReponse<GetServiceCallJobsResponse>> GetMyJobs(PaginationRequest<GetUserJobsRequest, GetServiceCallJobsResponse> request, CancellationToken cancellationToken)
        {
            // what status for service call ?
            var filteredContractItems = _serviceCallRepository.GetAllAsQuarble(x => x.UserId == request.Filters.UserId && x.ContractItem.Status == (int)JobStatusEnum.Remake)
                                        .Include(c => c.ContractItem.Contract)
                                        .Select(x => _mapper.Map<GetServiceCallJobsResponse>(x));

            return new PagedReponse<GetServiceCallJobsResponse>(filteredContractItems, await filteredContractItems.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
        }
    }
}
