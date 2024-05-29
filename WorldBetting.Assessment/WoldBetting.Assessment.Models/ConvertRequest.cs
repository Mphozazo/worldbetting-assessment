namespace WoldBetting.Assessment.Models
{
    public class ConvertRequest
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}
