using ASAP.Domain.Entities;
using ASAP.Domain.Repositories.Common;

namespace ASAP.Domain.Repositories
{
    public interface IContractItemRepository : IBaseEntityRepository<ContractItem>
    {
        IQueryable<Domain.Entities.ContractItem> GetFilteredContractItems(int pageNumber, int pageSize, int? contractItemCountId, Guid? contaractId, int? ProductionWeek, string? address = null, DateTime? installationDateFrom = null, DateTime? installationDateTo = null);
    }
}
