namespace WoldBetting.Assessment.Models
{
    public class BaseResponse : Base
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
