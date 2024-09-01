using ASAP.Infrastructure.Intergrations.Dtos;

namespace ASAP.Infrastructure.Intergrations
{
    public interface IPolygonIntergrationService
    {
        Task<PolygonResponse> FetchAndStoreStockDataAsync();
    }
}
