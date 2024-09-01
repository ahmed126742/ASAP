using ASAP.Application.Services;
using ASAP.Application.Services.Stock.DTOs;
using ASAP.Infrastructure.EmailServices;
using ASAP.Infrastructure.Intergrations;
using ASAP.Infrastructure.Intergrations.Dtos;
using AutoMapper;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace ASAP.Infrastructure.BackgroudServices
{
    public class StockBackgroundService : IStockBackgroundService
    {
        private readonly IMapper _mapper;
        private readonly IRecurringJobManager _recurringJobManager;

        private readonly IServiceProvider _serviceProvider;

        public StockBackgroundService(
            IMapper mapper,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _recurringJobManager = recurringJobManager;
            _serviceProvider = serviceProvider;
        }

        // better to event
        public async Task FetchNotifyAndStoreStockDataAsync()
        {
            // this should have been implmented as  publisher 
            _recurringJobManager.AddOrUpdate("ExcuteHourly", () => Excute(), Cron.HourInterval(6));
        }


        public async Task Excute()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _polygonIntergrationService = scope.ServiceProvider.GetRequiredService<IPolygonIntergrationService>();

                    var polygonResponse = await _polygonIntergrationService.FetchAndStoreStockDataAsync();
                    if (polygonResponse.ResultsCount == 0)
                        return;

                    // the following should have been implmented as  subscriber 
                    // var polygon = new Polygon { c = 1.5m , h=1.5m , L=1.5m , n = 1.5m , o= 1.5m , t=1231313, T ="egx30", v=123451, vw= 1.5m }
                    var _stockService = scope.ServiceProvider.GetRequiredService<IStockService>();
                    // Use the business service here
                    var stockRequest = _mapper.Map<List<StockRequestDto>>(polygonResponse.results);
                    await _stockService.AddStocksAsync(stockRequest, CancellationToken.None);

                    var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                    var emails = await _userService.GetUsersEmails(CancellationToken.None);

                    var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    await _emailService.NotifyClientWithUpdates(emails, CancellationToken.None);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

