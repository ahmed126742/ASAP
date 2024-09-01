using ASAP.Infrastructure.Intergrations.Dtos;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;

namespace ASAP.Infrastructure.Intergrations
{
    public class PolygonIntergration : IPolygonIntergrationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public PolygonIntergration(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<PolygonResponse> FetchAndStoreStockDataAsync()
        {
            var api_key = "YXOzNs6YbppoU70DFRYC_XdEj_I7uQZu"; //recommended to be encreapted in db ;
            var url = $"https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/2023-01-09?adjusted=true&apiKey={api_key}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"integration exception : status code : {response.StatusCode}");

            return  await response.Content.ReadAsAsync<PolygonResponse>();
        }
    }


}
