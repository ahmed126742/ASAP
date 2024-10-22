using ASAP.Application.Common.Models;
using ASAP.Application.Services.Contract.DTOs.Processing;
using ASAP.Application.Services.Contract.DTOs.Retreival;

namespace ASAP.Application.Services.Contract
{
    public interface IContractService
    {

        Task<Guid> CreateContract(CreateContractRequest request, CancellationToken cancellationToken);

        Task DeleteContractAsync(DeleteContractRequest request, CancellationToken cancellationToken);

        Task<GetContractResponse> GetContractAsync(GetContractRequest request, CancellationToken cancellationToken);

        Task<PagedReponse<GetFilteredContractsResponse>> GetPagedFilteresContracts(PaginationRequest<GetFilteredContractsRequest, GetFilteredContractsResponse> request, CancellationToken CancellationToken);

        Task UpdateContractAsync(UpdateContractRequest request, CancellationToken cancellationToken);
    }
}
