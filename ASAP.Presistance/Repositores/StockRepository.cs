using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;

namespace ASAP.Presistance.Repositores
{
    public class StockRepository : BaseEntityRepository<Stock>, IStockRepository
    {
        public StockRepository(DataContext _context) : base(_context) { }

        public async Task AddStocksAsync(IList<Stock> stocks, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Set<Stock>().AddRangeAsync(stocks, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
