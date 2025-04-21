using ASAP.Application.Common.Enums;
using ASAP.Application.Common.Models;
using ASAP.Application.Services.Contract.DTOs;
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
        private readonly IServiceCallRepository _serviceCallRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IFittingRepository _fittingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ContractItemService(
            IContractItemRepository contractItemRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IFittingRepository fittingRepository,
            IServiceCallRepository serviceCallRepository,
            ISurveyRepository surveyRepository)
        {
            _contractItemRepository = contractItemRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fittingRepository = fittingRepository;
            _surveyRepository = surveyRepository;
            _serviceCallRepository = serviceCallRepository;
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

            var greatestContractItemNumber = await _contractItemRepository.GetAllAsQuarble()
                .OrderByDescending(x => x.ContractItemNumber)
                .FirstOrDefaultAsync(cancellationToken);

            var contractItem = _mapper.Map<ContractItem>(request);
            if (greatestContractItemNumber == null)
                contractItem.ContractItemNumber = 1;
            else
                contractItem.ContractItemNumber = greatestContractItemNumber.ContractItemNumber + 1;
           
            _contractItemRepository.Create(contractItem);
            await _unitOfWork.Save(cancellationToken);
            return contractItem.Id;
        }

        public async Task DeleteContractItem(ContractItemIdentity request, CancellationToken cancellationToken)
        {
            var contractItem = await _contractItemRepository.GetContractItem(request.Id, cancellationToken);
            await _fittingRepository.DeleteFittingByContractItem(request.Id, cancellationToken);
            await _serviceCallRepository.DeleteServiceCallByContractItem(request.Id, cancellationToken);
            await _surveyRepository.DeleteSurveyByContractItem(request.Id, cancellationToken);
            _contractItemRepository.Delete(contractItem);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<GetContractItemResponse> GetContractItem(ContractItemIdentity request, CancellationToken cancellationToken)
        {
            var contractItem = await _contractItemRepository.GetAllAsQuarble(x => x.Id == request.Id)
                .Include(c=>c.Contract)
                .FirstOrDefaultAsync(cancellationToken);
            if (contractItem == null)
                throw new Exception("Production Id does not exist");

            return _mapper.Map<GetContractItemResponse>(contractItem);
        }

        public async Task<GetContractItemCountResponse> GetContractItemsCounts(ContractIdentityDto request, CancellationToken cancellationToken)
        {
            var result = new GetContractItemCountResponse();
            var contractItemsStatus = await _contractItemRepository.GetAllAsQuarble()
                .Where(x=> x.ContractId == request.Id)
                .Select(x => x.Status)
                .ToListAsync(cancellationToken);

            foreach (var contractItemStatus in contractItemsStatus)
            {
                result.ViewAll = result.ViewAll + 1;
                result.ViewCompleted = contractItemStatus == (int)JobStatusEnum.Complete ? result.ViewCompleted + 1 : result.ViewCompleted + 0;
                result.ViewRemake = contractItemStatus == (int)JobStatusEnum.Remake ? result.ViewRemake + 1 : result.ViewRemake + 0;
                result.ViewInComplete = contractItemStatus != (int)JobStatusEnum.Complete ? result.ViewInComplete + 1 : result.ViewInComplete + 0;
                result.ViewOnHold = contractItemStatus == (int)JobStatusEnum.OnHold ? result.ViewOnHold + 1 : result.ViewOnHold + 0;
            }
            return result;
        }

        public async Task<PagedReponse<GetFilteredContractItemReponse>> GetContractItemFiltered(PaginationRequest<GetFilteredContractItemRequest, GetFilteredContractItemReponse> request, CancellationToken cancellationToken)
        {
            var filteredContractItems = _contractItemRepository.GetFilteredContractItems(request.PageNumber, request.PageSize, (int)request.Filters.ContractItemCountId, request.Filters.ContractId, request.Filters.ProductionWeek, request.Filters.Address, request.Filters.InstallationDateFrom, request.Filters.InstallationDateTo, request.Filters.RequestDateFrom, request.Filters.RequestDateTo, request.Filters.GlassDeliveryDateFrom, request.Filters.GlassDeliveryDateTo);

            var paginatedFilteredContractItems = filteredContractItems.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(x=> x.Contract)
                .Select(x => _mapper.Map<GetFilteredContractItemReponse>(x));

            return new PagedReponse<GetFilteredContractItemReponse>(paginatedFilteredContractItems, await filteredContractItems.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
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
