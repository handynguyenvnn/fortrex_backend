namespace Web.SourceCoin.Models.Lendings
{
    public class PackagesResponse
    {
        public decimal NumberCoin { get; set; }
        public decimal PriceUsd { get; set; }
        public int Packages { get; set; }
        public bool Success { get; set; }
    }
}