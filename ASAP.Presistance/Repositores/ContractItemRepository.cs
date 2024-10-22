using System.Linq.Expressions;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;

namespace ASAP.Presistance.Repositores
{
    public class ContractItemRepository : BaseEntityRepository<ContractItem>, IContractItemRepository
    {
        public ContractItemRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Domain.Entities.ContractItem> GetFilteredContractItems(int pageNumber, int pageSize,Guid? contaractId , DateTime? installationDateFrom = null, DateTime? installationDateTo = null)
        {
            return GetQuarableAsPaginated(pageNumber, pageSize, FilterContractItems(contaractId, installationDateFrom, installationDateTo));
        }

        private Expression<Func<Domain.Entities.ContractItem, bool>> FilterContractItems(Guid? contaractId, DateTime? installationDateFrom = null, DateTime? installationDateTo = null)
        {
            return x => (contaractId == null || x.ContractId == contaractId)
            && (installationDateFrom == null || installationDateFrom <= x.InstallationDateFrom)
            && (installationDateTo == null || installationDateTo >= x.InstallationDateTo);
        }
    }
}
