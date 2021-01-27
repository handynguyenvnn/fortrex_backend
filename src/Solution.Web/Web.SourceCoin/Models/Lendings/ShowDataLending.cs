namespace Web.SourceCoin.Models.Lendings
{
    public class ShowDataLending
    {
        public decimal MoneyBeh { get; set; }
        public decimal MoneyUsd { get; set; }
        public bool Enable2Fa { get; set; }
        public bool EnableButton { get; set; }
        public int UserId { get; set; }
        public decimal TotalLending { get; set; }
        public decimal MoneyLending { get; set; }
        public decimal TotalKeepIn { get; set; }
        public decimal MoneyKeepIn { get; set; }
        public decimal TodayEarn { get; set; }
    }
}