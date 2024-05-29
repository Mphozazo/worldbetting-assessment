using Microsoft.Extensions.Configuration;


namespace WoldBetting.Assessment.Models
{
    public class ExchangeRateSettingsConfig
    {
        public string BaseAddress { get; set; }
        public string Accept { get; set; }
        public string ApiKey { get; set; }
    }

    public class HttpClientSettings
    {
        public ExchangeRateSettingsConfig ExchangeRateSettingsConfig { get; set; }
    }
}
