using ASAP.Domain.Entities;
using ASAP.Domain.Repositories.Common;

namespace ASAP.Domain.Repositories
{
    public interface IContractorRepository : IBaseEntityRepository<Contractor>
    {
        IQueryable<Contractor> GetFilteredContractors(int page, int rows, string? searchText = "");
    }
}
