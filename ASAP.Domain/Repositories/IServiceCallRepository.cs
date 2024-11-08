using ASAP.Domain.Entities;
using ASAP.Domain.Repositories.Common;

namespace ASAP.Domain.Repositories
{
    public interface IServiceCallRepository :IBaseEntityRepository<ServiceCall>
    {
        Task<ServiceCall> GetServiceCallByContractItemAsync(Guid contractItemId, CancellationToken cancellationToken);
    }
}
