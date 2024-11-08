using System.Linq.Expressions;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;
using Azure;

namespace ASAP.Presistance.Repositores
{
    public class ContractItemRepository : BaseEntityRepository<ContractItem>, IContractItemRepository
    {
        public ContractItemRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Domain.Entities.ContractItem> GetFilteredContractItems(int pageNumber, int pageSize, int? contractItemCountId, Guid? contaractId , int? ProductionWeek, string? address = null, DateTime? installationDateFrom = null, DateTime? installationDateTo = null)
        {
            var filterContractItems = GetAllAsQuarble(FilterContractItems(contractItemCountId, contaractId, ProductionWeek, address, installationDateFrom, installationDateTo))
                .OrderBy(x => x.Address);

            return filterContractItems;
        }

        private Expression<Func<Domain.Entities.ContractItem, bool>> FilterContractItems(int? contractItemCountId, Guid? contaractId, int? ProductionWeek, string? address = null,DateTime ? installationDateFrom = null, DateTime? installationDateTo = null)
        {
            return x => 
               (contractItemCountId == null || contractItemCountId == 1 // all
            || (contractItemCountId == 2 && x.Status == 8) // completed
            || (contractItemCountId == 3 && x.Status == 6) // remake
            || (contractItemCountId == 4 && x.Status != 8) // incomplete
            || (contractItemCountId == 5 && x.Status == 9)) // onHold
            && (contaractId == null || x.ContractId == contaractId)
            && (ProductionWeek == null || x.ProductionWeek == ProductionWeek)
            && (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(x.Address) || x.Address.ToLower().Contains(address.ToLower()))
            && (installationDateFrom == null || installationDateFrom <= x.InstallationDateFrom)
            && (installationDateTo == null || installationDateTo >= x.InstallationDateTo);
        }
    }
}
