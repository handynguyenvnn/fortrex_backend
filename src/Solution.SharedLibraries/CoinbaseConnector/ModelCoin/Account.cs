using CoinbaseConnector.ModelCoin.Base;

namespace CoinbaseConnector.ModelCoin
{
    public class Account
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool primary { get; set; }
        public string type { get; set; }
        public Currency currency { get; set; }
        public Balance balance { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string resource { get; set; }
        public string resource_path { get; set; }
        public Balance native_balance { get; set; }
    }
}
