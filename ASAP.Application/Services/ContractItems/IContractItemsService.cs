using ASAP.Application.Common.Models;
using ASAP.Application.Services.Contract.DTOs;
using ASAP.Application.Services.ContractItems.DTOs;
using ASAP.Application.Services.ContractItems.DTOs.Processing;
using ASAP.Application.Services.ContractItems.DTOs.Retrieval;

namespace ASAP.Application.Services.ContractItems
{
    public interface IContractItemsService
    {
        Task<Guid> CreateContractItem(CreateContractItemRequest request, CancellationToken cancellationToken);
        Task<GetContractItemResponse> GetContractItem(ContractItemIdentity request, CancellationToken cancellationToken);
        Task<GetContractItemCountResponse> GetContractItemsCounts(ContractIdentityDto request, CancellationToken cancellationToken);
        Task<PagedReponse<GetFilteredContractItemReponse>> GetContractItemFiltered(PaginationRequest<GetFilteredContractItemRequest, GetFilteredContractItemReponse> request, CancellationToken cancellationToken);
        Task UpdateContractItem(UpdateContractItemRequest request, CancellationToken cancellationToken);
        Task DeleteContractItem(ContractItemIdentity request, CancellationToken cancellationToken);
    }
}
