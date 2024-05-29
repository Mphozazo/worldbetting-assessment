namespace WoldBetting.Assessment.Models
{
    public class ConvertResponse : Base
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
