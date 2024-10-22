using ASAP.Application.Common.Models;
using ASAP.Application.Services.ContractItems.DTOs;
using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Fitting.DTOs.Processing;
using ASAP.Application.Services.User.Fitting.DTOs.Retrieval;
using ASAP.Application.Services.User.Survey.DTOs.Processing;
using ASAP.Application.Services.User.Survey.DTOs.Retrieval;

namespace ASAP.Application.Services.User.Fitting
{
    public interface IFitterService : IUseJobService
    {
        Task<Guid> CreateFitting(CreateFittingRequest request, CancellationToken cancellationToken);

        Task<GetFittingResponse> GetFitting(GetFittingRequest request, CancellationToken cancellationToken);

        Task<PagedReponse<GetFittingResponse>> GetFittings(PaginationRequest<GetFittingsRequest, GetFittingResponse> request, CancellationToken cancellationToken);

        Task<IList<GetUserJobsResponse>> GetMyFittings(GetUserJobsRequest request,CancellationToken cancellationToken);

        Task UpdateFitting(UpdateFittingRequest request, CancellationToken cancellationToken);

        Task DeleteFitting(DeleteFittingRequest request, CancellationToken cancellationToken);
    }
}
