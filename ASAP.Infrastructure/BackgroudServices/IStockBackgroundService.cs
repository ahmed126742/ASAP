namespace ASAP.Infrastructure.BackgroudServices
{
    public interface IStockBackgroundService
    {
        Task FetchNotifyAndStoreStockDataAsync();
    }
}
