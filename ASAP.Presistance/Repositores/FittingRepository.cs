using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;

namespace ASAP.Presistance.Repositores
{
    public class FittingRepository : BaseEntityRepository<Fitting>, IFittingRepository
    {
        public FittingRepository(DataContext context) : base(context)
        {
        }
    }
}
