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
                result.ViewInComplete = contractItemStatus == 8 ? result.ViewInComplete + 1 : result.ViewInComplete + 0;
                result.ViewRemake = contractItemStatus == 6 ? result.ViewRemake + 1 : result.ViewRemake + 0;
                result.ViewInComplete = contractItemStatus != 8 ? result.ViewInComplete + 1 : result.ViewInComplete + 0;
                result.ViewOnHold = contractItemStatus == 9 ? result.ViewOnHold + 1 : result.ViewOnHold + 0;
            }
            return result;
        }

        public async Task<PagedReponse<GetFilteredContractItemReponse>> GetContractItemFiltered(PaginationRequest<GetFilteredContractItemRequest, GetFilteredContractItemReponse> request, CancellationToken cancellationToken)
        {
            var filteredContractItems = _contractItemRepository.GetFilteredContractItems(request.PageNumber, request.PageSize, (int)request.Filters.ContractItemCountId, request.Filters.ContractId, request.Filters.ProductionWeek, request.Filters.Address, request.Filters.InstallationDateFrom, request.Filters.InstallationDateTo);

            var paginatedFilteredContractItems = filteredContractItems.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(x=> x.Contract)
                .Select(x => _mapper.Map<GetFilteredContractItemReponse>(x));

            return new PagedReponse<GetFilteredContractItemReponse>(paginatedFilteredContractItems, await filteredContractItems.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
            //var pagedResponse = new PagedReponse<GetFilteredContractItemReponse>(paginatedFilteredContractItems, await filteredContractItems.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
            //var suppliersIds = new List<Guid>();
            //foreach (var item in pagedResponse.Items)
            //{
            //    suppliersIds.Add(item.PD_SupplierId.GetValueOrDefault());
            //    suppliersIds.Add(item.Ancils_SupplierId.GetValueOrDefault());
            //    suppliersIds.Add(item.Bifolds_SupplierId.GetValueOrDefault());
            //    suppliersIds.Add(item.FED_SupplierId.GetValueOrDefault());
            //    suppliersIds.Add(item.Roofs_SupplierId.GetValueOrDefault());
            //    suppliersIds.Add(item.VS_SupplierId.GetValueOrDefault());
            //    suppliersIds.Add(item.W_RD_FD_SupplierId.GetValueOrDefault());
            //}
            //var suppliers = await _userRepository.GetAllAsQuarble(x => suppliersIds.Distinct().Contains(x.Id))?.ToListAsync(cancellationToken);

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
