using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using WoldBetting.Assessment.Models;
using WoldBetting.Assessment.Models.Interfaces;

namespace WorldBetting.Assessment.Core
{
    public class CurrencyConverterServiceAsync : ICurrencyConverterAsync
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;
        private readonly ILogger<CurrencyConverterServiceAsync> logger;

        public CurrencyConverterServiceAsync(ILogger<CurrencyConverterServiceAsync> _logger ,  IOptions<HttpClientSettings> _settings, IHttpClientFactory _httpClientFactory)
        {
            httpClient = _httpClientFactory.CreateClient("ExchangeRateApi");
            apiKey = _settings.Value.ExchangeRateSettingsConfig.ApiKey;
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            try
            {

                // first check if the cache still has the currency available 

                // only do this when cache doesn't have currency available
                #warning HttpClient not initiated corerctly to call the API 
                var response = await httpClient.GetStringAsync($"/latest/{apiKey}/{fromCurrency}");
                var data = JsonSerializer.Deserialize<ExchangeRateResponse>(response);

                return data == null ? throw new Exception("Unable to extract the exchange currency as data is null.") : data.Rates[toCurrency];
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
            }
            return 0;
        }

        public Task<bool> SaveHistoryToDatabase(string ToCurency, decimal exRate)
        {
            // unable to get this tested as my MySQL is not working and not aving time to fix it . 
            throw new NotImplementedException();
        }
    }
}
