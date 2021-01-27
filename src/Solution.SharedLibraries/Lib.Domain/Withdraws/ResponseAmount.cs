using Lib.Domain.Simples;

namespace Lib.Domain.Withdraws
{
    public class ResponseAmount : ResponseError
    {
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal Coin { get; set; }
        public decimal Invest { get; set; }
    }
}
