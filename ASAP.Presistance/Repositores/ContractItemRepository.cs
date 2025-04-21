using System.Linq.Expressions;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;
using Azure;
using Azure.Core;

namespace ASAP.Presistance.Repositores
{
    public class ContractItemRepository : BaseEntityRepository<ContractItem>, IContractItemRepository
    {
        public ContractItemRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Domain.Entities.ContractItem> GetFilteredContractItems(int pageNumber, int pageSize, int? contractItemCountId, Guid? contaractId, int? ProductionWeek, string? address = null, DateTime? installationDateFrom = null, DateTime? installationDateTo = null, DateTime? RequestDateFrom = null, DateTime? RequestDateTo = null, DateTime? GlassDeliveryDateFrom = null, DateTime? GlassDeliveryDateTo = null)
        { 
            var filterContractItems = GetAllAsQuarble(FilterContractItems(contractItemCountId, contaractId, ProductionWeek, address, installationDateFrom, installationDateTo, RequestDateFrom, RequestDateTo, GlassDeliveryDateFrom, GlassDeliveryDateTo))
                .OrderBy(x => x.Address);

            return filterContractItems;
        }

        private Expression<Func<Domain.Entities.ContractItem, bool>> FilterContractItems(int? contractItemCountId, Guid? contaractId, int? ProductionWeek, string? address = null,DateTime ? installationDateFrom = null, DateTime? installationDateTo = null, DateTime? RequestDateFrom = null, DateTime? RequestDateTo = null, DateTime? GlassDeliveryDateFrom = null, DateTime? GlassDeliveryDateTo = null)
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
            && (installationDateTo == null || installationDateTo >= x.InstallationDateTo)
            && (RequestDateFrom == null || RequestDateTo == null || (RequestDateFrom <= x.RequestDate && RequestDateTo >= x.RequestDate))
            && (GlassDeliveryDateFrom == null || GlassDeliveryDateTo == null|| (GlassDeliveryDateFrom <= x.GlassDeliveryDate && GlassDeliveryDateTo >= x.GlassDeliveryDate));
        }

        public async Task<ContractItem> GetContractItem(Guid id, CancellationToken cancellationToken)
        {
            var contractItem = await Get(id, cancellationToken);
            if (contractItem == null)
                throw new Exception("Production Id does not exist");

            return contractItem;
        }
    }
}
