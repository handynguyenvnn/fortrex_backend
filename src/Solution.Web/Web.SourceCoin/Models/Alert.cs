namespace Web.SourceCoin.Models
{
    public class Alert
    {
        public Alert()
        {
            Success = false;
            EnableAuthy = false;
            UserId = 0;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
        public string ClassCss { get; set; }
        public int UserId { get; set; }
        public object Reply { get; set; }
        public string Token { get; set; }
        public bool EnableAuthy { get; set; }
    }
}