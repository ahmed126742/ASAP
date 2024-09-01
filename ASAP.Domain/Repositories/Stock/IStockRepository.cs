using ASAP.Domain.Entities;

namespace ASAP.Domain.Repositories
{
    public interface IStockRepository
    {
        Task AddStocksAsync(IList<Stock> stocks, CancellationToken cancellationToken);
    }
}
