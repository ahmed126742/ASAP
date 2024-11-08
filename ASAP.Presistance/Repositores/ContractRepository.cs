using System.Linq.Expressions;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Presistance.Repositores
{
    public class ContractRepository : BaseEntityRepository<Domain.Entities.Contract>, IContractRepository
    {
        public ContractRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Domain.Entities.Contract> GetFilteredContracts(int pageNumber, int pageSize, string? searchText = null, int? ContractTypeId = null, bool? IsArchived = null, string? Address = null)
        {
            return GetQuarableAsPaginated(pageNumber, pageSize, FilterContracts(searchText, ContractTypeId, IsArchived, Address));
        }

        private Expression<Func<Domain.Entities.Contract, bool>> FilterContracts(string? searchText = null, int? ContractTypeId = null, bool? IsArchived = null, string? Address = null)
        {
            return x => (string.IsNullOrEmpty(searchText) || x.Name.ToLower().Contains(searchText.ToLower()))
            && (IsArchived == null || x.IsArchived == IsArchived)
            && (ContractTypeId == null || ContractTypeId == 0 || x.ContractTypeId == ContractTypeId)
            && (string.IsNullOrEmpty(Address) || x.Address.ToLower().Contains(Address.ToLower())
            || x.contractItems.Any(x => x.Address.ToLower().Contains(Address)));
        }
    }
}
