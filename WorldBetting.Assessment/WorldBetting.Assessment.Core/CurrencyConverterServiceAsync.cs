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

        public CurrencyConverterServiceAsync(IOptions<HttpClientSettings> _settings, IHttpClientFactory _httpClientFactory)
        {
            httpClient = _httpClientFactory.CreateClient("ExchangeRateApi");
            apiKey = _settings.Value.ExchangeRateSettingsConfig.ApiKey;
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {

            var response = await httpClient.GetStringAsync($"/latest/{apiKey}/{fromCurrency}");



            var data = JsonSerializer.Deserialize<ExchangeRateResponse>(response);
            return data.Rates[toCurrency];
        }

        private class ExchangeRateResponse
        {
            public Dictionary<string, decimal> Rates { get; set; }
        }
    }
}
