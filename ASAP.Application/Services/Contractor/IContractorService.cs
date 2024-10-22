using ASAP.Application.Common.Models;
using ASAP.Application.Services.Contractor.DTOs.Processing;
using ASAP.Application.Services.Contractor.DTOs.Retreival;

namespace ASAP.Application.Services.Contractor
{
    public interface IContractorService
    {
        Task<Guid> CreateContractor(CreateContractorDto request, CancellationToken cancellationToken);

        Task<GetContractorResponse> GetContractorAsync(GetContractorRequest request, CancellationToken cancellationToken);

        Task<PagedReponse<GetFilteredContractorsResponse>> GetPagedFilteresContractors(PaginationRequest<GetFilteredContractorsRequest, GetFilteredContractorsResponse> request, CancellationToken CancellationToken);

        Task UpdateContractorAsync(UpdateContractorDto request, CancellationToken cancellationToken);

        Task DeleteContractorAsync(DeleteContractorRequest request, CancellationToken cancellationToken);
    }
}
