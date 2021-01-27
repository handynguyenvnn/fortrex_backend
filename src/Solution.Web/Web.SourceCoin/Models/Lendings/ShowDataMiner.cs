namespace Web.SourceCoin.Models.Lendings
{
    public class ShowDataMiner
    {
        public decimal BalanceBeh { get; set; }
        public decimal MoneyBeh { get; set; }
        public bool Enable2Fa { get; set; }
        public bool EnableButton { get; set; }
        public int UserId { get; set; }
        public decimal TotalMiner { get; set; }
        public decimal MoneyMiner { get; set; }
        public decimal TodayEarn { get; set; }
    }
}