namespace Web.SourceCoin.Models.Dashboards
{
    public class Dashboard
    {
        public int UserId { get; set; }
        public string UrlShare { get; set; }
        public decimal PriceUSD { get; set; }
        public decimal PriceBTC { get; set; }
        public decimal PriceETH { get; set; }
        public decimal PriceBTH { get; set; }
    }

    public class WithdrawModal
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string codeDigit { get; set; }
        
    }
    public class WithdrawConfirmEmailModal
    {
        public string Token { get; set; }
    }
}