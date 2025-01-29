using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Presistance.Repositores
{
    public class SurveyRepository : BaseEntityRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(DataContext context) : base(context)
        {
        }

        public async Task DeleteSurveyByContractItem(Guid contractItemId, CancellationToken cancellationToken)
        {
            var surveys = await GetAllAsQuarble(x => x.ContractItemId == contractItemId).ToListAsync(cancellationToken);
            if (!surveys.Any())
                return;

            _context.RemoveRange(surveys);
        }
    }
}
