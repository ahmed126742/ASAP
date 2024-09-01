using ASAP.Application.Services.Stock.DTOs;

namespace ASAP.Application.Services
{
    public interface IStockService
    {
        Task AddStocksAsync(IList<StockRequestDto> stocks, CancellationToken cancellationToken);
    }
}
