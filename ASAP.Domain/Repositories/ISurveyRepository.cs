using ASAP.Domain.Entities;
using ASAP.Domain.Repositories.Common;

namespace ASAP.Domain.Repositories
{
    public interface ISurveyRepository : IBaseEntityRepository<Survey>
    {
        Task DeleteSurveyByContractItem(Guid contractItemId, CancellationToken cancellationToken);
    }
}
