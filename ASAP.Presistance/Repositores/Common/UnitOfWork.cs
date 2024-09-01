using ASAP.Domain.Repositories.Common;
using ASAP.Presistance.Contexts;

namespace ASAP.Presistance.Repositores.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
