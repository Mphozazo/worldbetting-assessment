using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoldBetting.Assessment.Models;
using WorldBetting.Assessment.Core;
using Xunit.Priority;

namespace WorldBetting.Assessment.WebAPI.Tests
{
    public class CurrencyExchangeServiceTest
    {
        //Want to test if we get the correct currency when we call currency exchange either from cache or API.
        [Theory(Skip = "Intergration Test"), AutoMoqData, Priority(1)]
        public async void When_Get_Currency_Exchange_Value_Called_EchangeCurrency(ILogger<CurrencyConverterServiceAsync> mocklogger , 
            IHttpClientFactory mockhttpclient ,
            IOptions<HttpClientSettings> mocksettings
            )
        {
            var mockCurencyExchanger  = new CurrencyConverterServiceAsync(mocklogger, mocksettings, mockhttpclient);

            var actual = await mockCurencyExchanger.GetExchangeRateAsync("USD","ZAR");
            // check on 30th @ 07:30
            Assert.True(actual.Equals(18.3426));

        }

        [Theory(Skip = "Intergration Test"), AutoMoqData, Priority(2)]
        public async void When_Save_Currency_Exchange_Rate_Value_Into_History_Table_DB(ILogger<CurrencyConverterServiceAsync> mocklogger,
           IHttpClientFactory mockhttpclient,
           IOptions<HttpClientSettings> mocksettings
           )
        {
            var mockCurencyExchanger = new CurrencyConverterServiceAsync(mocklogger, mocksettings, mockhttpclient);

            var actual = await mockCurencyExchanger.SaveHistoryToDatabase("ZAR" , Convert.ToDecimal(18.34));
            // value on 30th @ 07:30
            Assert.True(actual == true);

        }

    }
}
