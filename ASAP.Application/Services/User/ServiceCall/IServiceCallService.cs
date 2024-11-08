using ASAP.Application.Common.Models;
using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.ServiceCall.DTOs;
using ASAP.Application.Services.User.ServiceCall.DTOs.Processing;
using ASAP.Application.Services.User.ServiceCall.DTOs.Retireval;
using ASAP.Application.Services.User.Survey.DTOs.Processing;

namespace ASAP.Application.Services.User.ServiceCall
{
    public interface IServiceCallService
    {
        Task<Guid> CreateServiceCall(CreateServiceCallRequest request, CancellationToken cancellationToken);
        Task CompleteServiceCall(CompleteMyServiceCallRequest request, CancellationToken cancellationToken);
        Task<PagedReponse<GetServiceCallJobsResponse>> GetMyJobs(PaginationRequest<GetUserJobsRequest, GetServiceCallJobsResponse> request, CancellationToken cancellationToken);
        Task<GetServiceCallResponse> GetServiceCallByContractItem(ServiceCallIdentityDto request, CancellationToken cancellationToken);
        Task RevisitServiceCall(ServiceCallIdentityDto request, CancellationToken cancellationToken);
        Task<GetServiceCallResponse> GetServiceCall(ServiceCallIdentityDto request, CancellationToken cancellationToken);
        Task<PagedReponse<GetServiceCallResponse>> GetServiceCalls(PaginationRequest<GetServicesCallsRequest, GetServiceCallResponse> request, CancellationToken cancellationToken);
        Task UpdateServiceCall(UpdateServiceCallRequest request, CancellationToken cancellationToken);
        Task DeleteServiceCall(DeleteSurveyRequest request, CancellationToken cancellationToken);
        Task<PagedReponse<GetUserJobsResponse>> GetMyServiceCalls(PaginationRequest<GetUserJobsRequest, GetUserJobsResponse> request, CancellationToken cancellationToken);

    }
}
