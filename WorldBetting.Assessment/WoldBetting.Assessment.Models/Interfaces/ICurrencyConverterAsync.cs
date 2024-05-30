using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoldBetting.Assessment.Models.Interfaces
{
    public interface ICurrencyConverterAsync
    {
        Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency);

        Task<bool> SaveHistoryToDatabase(string ToCurency, decimal exRate);
    }
}
