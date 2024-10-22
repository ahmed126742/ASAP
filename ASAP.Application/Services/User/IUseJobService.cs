using ASAP.Application.Common.Models;
using ASAP.Application.Services.User.DTOs;

namespace ASAP.Application.Services.User
{
    public interface IUseJobService
    {
        Task<PagedReponse<GetUserJobsResponse>> GetMyJobs(PaginationRequest<GetUserJobsRequest, GetUserJobsResponse> request, CancellationToken cancellationToken);
    }
}
