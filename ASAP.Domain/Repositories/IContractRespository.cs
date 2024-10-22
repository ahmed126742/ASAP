using ASAP.Domain.Entities;
using ASAP.Domain.Repositories.Common;

namespace ASAP.Domain.Repositories
{
    public interface IContractRepository : IBaseEntityRepository<Entities.Contract>
    {
        IQueryable<Domain.Entities.Contract> GetFilteredContracts(int pageNumber, int pageSize, string? searchText = null, int? ContractTypeId = null, string? Address = null);
    }
}
