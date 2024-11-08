using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Presistance.Repositores
{
    public class ServiceCallRepository : BaseEntityRepository<ServiceCall>, IServiceCallRepository
    {
        public ServiceCallRepository(DataContext context) : base(context)
        {
        }

        public async Task<ServiceCall> GetServiceCallByContractItemAsync(Guid contractItemId, CancellationToken cancellationToken)
        {
             var serviceCall =  await GetAllAsQuarble().FirstOrDefaultAsync(c=>c.ContractItemId == contractItemId, cancellationToken);
            return serviceCall;
        }
    }
}
