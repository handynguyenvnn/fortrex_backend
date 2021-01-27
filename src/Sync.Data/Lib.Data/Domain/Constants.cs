using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain
{
    public static class Constants
    {
        public static TimeSpan TwoFaCodeExpire = TimeSpan.FromSeconds(50);
        public static string TYPECOIN_COIN = "COIN";
        public static string TYPECOIN_ERC20 = "ERC20";
        public static string TYPECOIN_ERC223 = "ERC223";
        public static string TYPECOIN_ERC721 = "ERC721";

        /// <summary>
        /// COIN LIST
        /// </summary>
        public static string COIN_SYMBOL_BTC = "BTC";
        public static string COIN_SYMBOL_ETH = "ETH";
        public static string COIN_SYMBOL_BNCT = "BNCT";
        public static string COIN_SYMBOL_USDT = "USDT";
        /// <summary>
        /// Coin Name
        /// </summary>
        public static string COIN_NAME_ETH = "Ethereum";
        public static string INTERVENTION_SYSTEM = "InterventionSystem";
        public static string INTERVENTION_SYSTEM_STOPLOSS = "InterventionSystem.StopLoss";
    }
}
