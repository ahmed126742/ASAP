using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Presistance.Repositores
{
    public class FittingRepository : BaseEntityRepository<Fitting>, IFittingRepository
    {
        public FittingRepository(DataContext context) : base(context)
        {
        }

        public async Task DeleteFittingByContractItem(Guid contractItemId, CancellationToken cancellationToken)
        {
            var fittings = await GetAllAsQuarble(x => x.ContractItemId == contractItemId).ToListAsync(cancellationToken);
            if (!fittings.Any())
                return;

            _context.RemoveRange(fittings);
            
        }
    }
}
