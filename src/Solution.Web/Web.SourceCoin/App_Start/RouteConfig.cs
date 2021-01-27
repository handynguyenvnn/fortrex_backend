using System.Web.Mvc;
using System.Web.Routing;

namespace Web.SourceCoin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            #region quan ly tai khoan
            // routes.MapRoute(
            //    name: "Whyinvestment",
            //    url: "Whyinvestment",
            //    defaults: new { controller = "Home", action = "Whyinvestment" }
            //);
            //  routes.MapRoute(
            //    name: "investment-strategies",
            //    url: "investment-strategies",
            //    defaults: new { controller = "Home", action = "Investmentstrategies" }
            //);
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Home", action = "Login" }
            );
            //routes.MapRoute(
            //    name: "LoginSocial",
            //    url: "loginsocial",
            //    defaults: new { controller = "Home", action = "LoginSocial" }
            //);
            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Home", action = "Register" }
            );
            routes.MapRoute(
                name: "ConfirmEmail",
                url: "confirm",
                defaults: new { controller = "Home", action = "ConfirmEmail" }
            );
           
            routes.MapRoute(
                name: "MailActive",
                url: "activate-mail",
                defaults: new { controller = "Home", action = "MailActive" }
            );
            routes.MapRoute(
                name: "ForgotPassword",
                url: "forgotpassword",
                defaults: new { controller = "Home", action = "ForgotPassword" }
            );
            routes.MapRoute(
                name: "GetChangePassword",
                url: "getpassword",
                defaults: new { controller = "Home", action = "GetChangePassword" }
            );
            //routes.MapRoute(
            //    name: "CryptoMining",
            //    url: "crypto-mining",
            //    defaults: new { controller = "Home", action = "CryptoMining" }
            //);
            // routes.MapRoute(
            //    name: "BuySell",
            //    url: "buy-sell",
            //    defaults: new { controller = "Home", action = "BuySell" }
            //);

            routes.MapRoute(
               name: "LiveMarket",
               url: "exchange",
               defaults: new { controller = "Home", action = "LiveMarket" }
           );
            // routes.MapRoute(
            //    name: "LastDeposit",
            //    url: "last-deposit",
            //    defaults: new { controller = "Home", action = "LastDeposit" }
            //);
            routes.MapRoute(
              name: "Contact",
              url: "contact",
              defaults: new { controller = "Home", action = "Contact" }
          );
            routes.MapRoute(
             name: "AboutUs",
             url: "aboutus",
             defaults: new { controller = "Home", action = "AboutUs" }
         );
            routes.MapRoute(
             name: "Options",
             url: "options",
             defaults: new { controller = "Home", action = "Options" }
         );
            routes.MapRoute(
             name: "Forex",
             url: "forex",
             defaults: new { controller = "Home", action = "Forex" }
         );
            routes.MapRoute(
             name: "Synthetic",
             url: "synthetic",
             defaults: new { controller = "Home", action = "Synthetic" }
         );
            routes.MapRoute(
             name: "Commodities",
             url: "commodities",
             defaults: new { controller = "Home", action = "Commodities" }
         );
            routes.MapRoute(
            name: "ForbitTrading",
            url: "forex-fb",
            defaults: new { controller = "Home", action = "ForbitTrading" }
        );
            routes.MapRoute(
                name: "TradeOptions",
                url: "options",
                defaults: new { controller = "Home", action = "TradeOptions" }
                );
            routes.MapRoute(
                name: "TradeCrypto",
                url: "crypto",
                defaults: new { controller = "Home", action = "TradeCrypto" }
                );
            routes.MapRoute(
              name: "Stocks",
              url: "stocks",
              defaults: new { controller = "Home", action = "Stocks" }
              );

            //     routes.MapRoute(
            //    name: "WhatisaPAMM",
            //    url: "What-is-a-PAMM",
            //    defaults: new { controller = "Home", action = "WhatisaPAMM" }
            //);
            //     routes.MapRoute(
            //      name: "WhatisaMAM",
            //      url: "What-is-a-MAM",
            //      defaults: new { controller = "Home", action = "WhatisaMAM" }
            //  );
            //     routes.MapRoute(
            //    name: "GoldenCross",
            //    url: "Golden-Cross",
            //    defaults: new { controller = "Home", action = "GoldenCross" }
            //);
            //     routes.MapRoute(
            //      name: "DeathCross",
            //      url: "Death-Cross",
            //      defaults: new { controller = "Home", action = "DeathCross" }
            //  );
            //     routes.MapRoute(
            //      name: "TradePool",
            //      url: "trade-pool",
            //      defaults: new { controller = "Home", action = "TradePools" }
            //  );
            //     routes.MapRoute(
            //      name: "AGI",
            //      url: "Forbit-Labs",
            //      defaults: new { controller = "Home", action = "AGI" }
            //  );
            //     routes.MapRoute(
            //       name: "HowItWorks",
            //       url: "how-it-works",
            //       defaults: new { controller = "Home", action = "HowItWorks" }
            //   );
            routes.MapRoute(
              name: "BlockChain",
              url: "blockchain",
              defaults: new { controller = "Home", action = "BlockChain" }
          );
            //     routes.MapRoute(
            //       name: "BigData",
            //       url: "big-data",
            //       defaults: new { controller = "Home", action = "BigData" }
            //   );
            //     routes.MapRoute(
            //       name: "Iot",
            //       url: "iot",
            //       defaults: new { controller = "Home", action = "Iot" }
            //   );
            routes.MapRoute(
             name: "FAQs",
             url: "faqs",
             defaults: new { controller = "Home", action = "FAQs" }
         );
            //     routes.MapRoute(
            //      name: "faq/terms",
            //      url: "faqs/terms",
            //      defaults: new { controller = "Home", action = "Terms" }
            //  );
            //    routes.MapRoute(
            //    name: "DailyBonus",
            //    url: "daily-bonus",
            //    defaults: new { controller = "Home", action = "DailyBonus" }
            //);
            routes.MapRoute(
                name: "RegisterLink",
                url: "register-by",
                defaults: new { controller = "Home", action = "RegisterLink" }
            );
            routes.MapRoute(
               name: "IntroducersBroker",
               url: "IntroducersBroker",
               defaults: new { controller = "Home", action = "IntroducersBroker" }
           );
            #endregion

            #region Admin
            routes.MapRoute(
               name: "office",
               url: "tradingroom",
               defaults: new { controller = "Office", action = "Index" }
           );

            routes.MapRoute(
            name: "TradePairs_Gets",
            url: "Trade/Pairs",
            defaults: new { controller = "Office", action = "TradePairs_Gets" }
        );
            routes.MapRoute(
            name: "Candlestick_Gets",
            url: "Trade/Candlesticks",
            defaults: new { controller = "Office", action = "Candlestick_Gets" }
        );

            routes.MapRoute(
                name: "UserProfile",
                url: "account/settings",
                defaults: new { controller = "Office", action = "UserProfile" }
            );
            routes.MapRoute(
                  name: "ServerTime",
                  url: "serverTime",
                  defaults: new { controller = "Home", action = "ServerTime" }
              );
            routes.MapRoute(
                 name: "bookorder",
                 url: "orders/book",
                 defaults: new { controller = "Office", action = "UserOrder" }
             );
            routes.MapRoute(
                name: "ChangePassword",
                url: "password-change",
                defaults: new { controller = "Office", action = "ChangePassword" }
            );
            routes.MapRoute(
                name: "OfficeGetPackage",
                url: "get-package",
                defaults: new { controller = "Office", action = "GetPackage" }
            );
            routes.MapRoute(
                name: "OfficeInvestment",
                url: "investment",
                defaults: new { controller = "Office", action = "Investment" }
            );
            routes.MapRoute(
                name: "OfficeInvestmentHistory",
                url: "investment-history",
                defaults: new { controller = "Office", action = "InvestmentHistory" }
            );
            routes.MapRoute(
                name: "OfficeMywallet",
                url: "mywallet",
                defaults: new { controller = "Office", action = "MyWallet" }
            );
            routes.MapRoute(
                name: "OfficeTransfer",
                url: "transfer",
                defaults: new { controller = "Office", action = "Transfer" }
            );
            //routes.MapRoute(
            //     name: "OfficeTransfer_From_Forbitoption",
            //     url: "transfer-money",
            //     defaults: new { controller = "Office", action = "Transfer_Money" }
            //);
            routes.MapRoute(
                name: "OfficeTransferUsername",
                url: "transfer-username",
                defaults: new { controller = "Office", action = "GetUsername" }
            );
            routes.MapRoute(
                name: "OfficeDeposit",
                url: "deposit",
                defaults: new { controller = "Office", action = "Deposit" }
            );
            routes.MapRoute(
                name: "OfficeWithdraw",
                url: "withdraw",
                defaults: new { controller = "Office", action = "Withdraw" }
            );
            routes.MapRoute(
                name: "OfficeWithdrawHistory",
                url: "withdraw-history",
                defaults: new { controller = "Office", action = "WithdrawHistory" }
            );
            routes.MapRoute(
                name: "OfficeWithdrawComfirm",
                url: "withdraw-confirm",
                defaults: new { controller = "Office", action = "Comfirm" }
            );
            routes.MapRoute(
                name: "operation",
                url: "account/trading-history",
                defaults: new { controller = "Office", action = "transaction" }
            );
            routes.MapRoute(
               name: "ArbittrageTransaction_Lst",
               url: "Arbittrage/TransactionLst",
               defaults: new { controller = "Office", action = "ArbittrageTransaction_Lst" }
           );
            routes.MapRoute(
                name: "OfficeLogDervice",
                url: "account/myauthor",
                defaults: new { controller = "Office", action = "LogDervice" }
            );
            routes.MapRoute(
                name: "manageaccount",
                url: "account/Affiliate",
                defaults: new { controller = "Office", action = "manageaccount" }
            );
            routes.MapRoute(
                name: "referral",
                url: "referral/list",
                defaults: new { controller = "Office", action = "referral" }
            );
            routes.MapRoute(
                name: "network",
                url: "account/treeview",
                defaults: new { controller = "Office", action = "network" }
            );
            routes.MapRoute(
               name: "networks",
               url: "account/treeview-find",
               defaults: new { controller = "Office", action = "networks" }
           );
            routes.MapRoute(
               name: "kyc",
               url: "account/kyc",
               defaults: new { controller = "Office", action = "kyc" }
           );
            routes.MapRoute(
               name: "TwoFactorAuthentication",
               url: "account/2fa",
               defaults: new { controller = "Office", action = "TwoFactorAuthentication" }
           );
            routes.MapRoute(
               name: "OfficeSecurity",
               url: "account/security",
               defaults: new { controller = "Office", action = "Security" }
           );

            #endregion
            routes.MapRoute(
               name: "Affiliate",
               url: "Affiliate",
               defaults: new { controller = "Home", action = "Affiliate" }
           );
            routes.MapRoute(
                name: "homeindex",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
