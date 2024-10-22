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

        public IQueryable<Domain.Entities.Contract> GetFilteredContracts(int pageNumber, int pageSize, string? searchText = null, int? ContractTypeId = null, string? Address = null)
        {
            return GetQuarableAsPaginated(pageNumber, pageSize, FilterContracts(searchText));
        }

        private Expression<Func<Domain.Entities.Contract, bool>> FilterContracts(string? searchText = null, int? ContractTypeId = null, string? Address = null)
        {
            return x => (string.IsNullOrEmpty(searchText) || x.Name.ToLower().Contains(searchText.ToLower()))
            && (ContractTypeId == null || x.ContractTypeId == ContractTypeId)
            && (string.IsNullOrEmpty(Address) || x.Address.ToLower().Contains(Address.ToLower()));
        }
    }
}
