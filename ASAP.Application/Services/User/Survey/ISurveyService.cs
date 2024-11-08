using ASAP.Application.Common.Models;
using ASAP.Application.Services.ContractItems.DTOs;
using ASAP.Application.Services.User.Survey.DTOs.Processing;
using ASAP.Application.Services.User.Survey.DTOs.Retrieval;

namespace ASAP.Application.Services.User.Survey
{
    public interface ISurveyService : IUseJobService
    {
        Task<Guid> CreateSurvey(CreateSurveyRequest request, CancellationToken cancellationToken);
        
        Task<GetSurveyResponse> GetSurvey(GetSurveyRequest request, CancellationToken cancellationToken);

        Task<PagedReponse<GetSurveyResponse>> GetSurveys(PaginationRequest<GetSurverysRequest, GetSurveyResponse> request, CancellationToken cancellationToken);

        Task<IList<GetSurveyResponse>> GetSurveysByContractItem(ContractItemIdentity request, CancellationToken cancellationToken);

        Task UpdateSurvey(UpdateSurveyRequest request, CancellationToken cancellationToken);

        Task DeleteSurvey(DeleteSurveyRequest request, CancellationToken cancellationToken);
    }
}
