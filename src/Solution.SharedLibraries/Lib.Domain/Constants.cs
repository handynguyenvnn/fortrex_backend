using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain
{
    public static class Constants
    {
        public static double DEFILE_ROUND = 1000000.0;
        public static string TOKEN_KEY = "FORTREX-3jGF-356A";
        public static int PAGE_INDEX = 0;
        public static int PAGE_SIZE = 1000;
        public static int PAGE_SIZE_DEFAULT = 20;
        public static TimeSpan TwoFaCodeExpire = TimeSpan.FromSeconds(50);
        public static string Coin_Price ="GES.Frice";
        public static string PAIR_DEFAULT = "BTC_USD";
        public static string PAIR_DEFAULT2 = " AUD_CAD";

        public static string TYPECOIN_COIN = "COIN";
        public static string TYPECOIN_ERC20 = "ERC20";
        public static string TYPECOIN_ERC223 = "ERC223";
        public static string TYPECOIN_ERC721 = "ERC721";
        public static string SORT_BY_ASC = "asc";
        public static string SORT_BY_DESC = "desc";

        /// <summary>
        /// COIN LIST
        /// </summary>
        public static string COIN_SYMBOL_BTC = "BTC";
        public static string COIN_SYMBOL_ETH = "ETH";
        public static string COIN_SYMBOL_BNCT = "BNCT";
        public static string COIN_SYMBOL_USDT = "USDT";
        public static string COIN_SYMBOL_GES = "GES";
        public static string COIN_SYMBOL_ELD = "ELD";
        public static string COIN_SYMBOL_BRI = "BRI";
        
        /// <summary>
        /// Coin Name
        /// </summary>
        public static string COIN_NAME_ETH = "Ethereum";
        public static string TRADE_NAME_COOKEY = "abb1223de453fvvvv887jhhh99sssfgg4";
    }
}
