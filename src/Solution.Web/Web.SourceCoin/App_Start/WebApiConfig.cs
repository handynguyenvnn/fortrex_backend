using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
//using System.Web.Http.Cors;
using Microsoft.Practices.Unity;

namespace Web.SourceCoin
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API routes
            config.MapHttpAttributeRoutes();
            // Web API configuration and services
            // xoa đoạn này đi, nó xạo đó.  chỉ cần cấu hình ở web.config
            //var cors = new EnableCorsAttribute("https://fortrex.io", "*", "*");
            //config.EnableCors(cors);
            config.EnableCors();

            config.Routes.MapHttpRoute(
               name: "AppGet_Server_Time",
               routeTemplate: "api/servertime",
               defaults: new { controller = "Webapi", action = "Get_Server_Time" }
            );

            config.Routes.MapHttpRoute(
               name: "AppGet_PairName_by_UserId",
               routeTemplate: "api/user/pairsfavorite",
               defaults: new { controller = "Webapi", action = "Get_PairName_by_UserId" }
            );

            config.Routes.MapHttpRoute(
               name: "AppTradePairs",
               routeTemplate: "api/martkets",
               defaults: new { controller = "Webapi", action = "AppTradePairs" }
            );

            config.Routes.MapHttpRoute(
               name: "AppPushOrder",
               routeTemplate: "api/user/order",
               defaults: new { controller = "Webapi", action = "PushOrder" }
           );

            config.Routes.MapHttpRoute(
               name: "AppLogin",
               routeTemplate: "api/user/login",
               defaults: new { controller = "Webapi", action = "Login" }
           );

            config.Routes.MapHttpRoute(
               name: "AppRegister",
               routeTemplate: "api/user/register",
               defaults: new { controller = "Webapi", action = "Register" }
           );

            config.Routes.MapHttpRoute(
               name: "AppPairName_Favorite_Ins",
               routeTemplate: "api/user/addpairfavorite",
               defaults: new { controller = "Webapi", action = "PairName_Favorite_Ins" }
            );

            config.Routes.MapHttpRoute(
               name: "AppGet_Balance_By",
               routeTemplate: "api/user/getbalance",
               defaults: new { controller = "Webapi", action = "Get_Balance_By" }
            );

            config.Routes.MapHttpRoute(
                name: "AppMarketPrice",
                routeTemplate: "api/ticket/price",
                defaults: new { controller = "Webapi", action = "AppMarketPrice" }
            );

            config.Routes.MapHttpRoute(
               name: "AppCandlestick",
               routeTemplate: "api/klines",
               defaults: new { controller = "Webapi", action = "AppCandlestick" }
            );

            config.Routes.MapHttpRoute(
              name: "AppTradingList",
              routeTemplate: "api/user/tradings",
              defaults: new { controller = "Webapi", action = "AppTradingList" }
           );
            config.Routes.MapHttpRoute(
              name: "AppTransactionHistoryList",
              routeTemplate: "api/user/transactions",
              defaults: new { controller = "Webapi", action = "AppTransactionHistoryList" }
           );
            config.Routes.MapHttpRoute(
               name: "AppForgotPassword",
               routeTemplate: "api/user/forgotpass",
               defaults: new { controller = "Webapi", action = "ForgotPassword" }
            );

            config.Routes.MapHttpRoute(
               name: "AppUpdatePassword",
               routeTemplate: "api/user/changepass",
               defaults: new { controller = "Webapi", action = "UpdatePassword" }
            );
            config.Routes.MapHttpRoute(
              name: "AppResetPassword",
              routeTemplate: "api/user/resetpassword",
              defaults: new { controller = "Webapi", action = "ResetPassword" }
           );
            config.Routes.MapHttpRoute(
               name: "AppWithdrawRequest",
               routeTemplate: "api/user/withdraw_request",
               defaults: new { controller = "Webapi", action = "Withdraw_Request" }
            );

            config.Routes.MapHttpRoute(
               name: "withdraw-submit",
               routeTemplate: "api/withdraw/submit",
               defaults: new { controller = "Webapi", action = "Withdraw_Ins" }
            );
            config.Routes.MapHttpRoute(
               name: "withdraw-confirmemail",
               routeTemplate: "api/withdraw/confirmemail",
               defaults: new { controller = "Webapi", action = "Withdraw_Confirm" }
            );
            config.Routes.MapHttpRoute(
               name: "Withdraw_History",
               routeTemplate: "api/withdraw/historys",
               defaults: new { controller = "Webapi", action = "Withdraw_History" }
            );
            config.Routes.MapHttpRoute(
             name: "Get_ReferralCode",
             routeTemplate: "api/user/affiliateGet",
             defaults: new { controller = "Webapi", action = "Get_Affiliate" }
          );
            config.Routes.MapHttpRoute(
             name: "Lst_Trading_Affiliate",
             routeTemplate: "api/user/affiliateGets",
             defaults: new { controller = "Webapi", action = "Lst_Trading_Affiliate" }
          );
            config.Routes.MapHttpRoute(
              name: "Deposit_Gets",
              routeTemplate: "api/deposit/gets",
              defaults: new { controller = "Webapi", action = "Deposit_Gets" }
           );
            config.Routes.MapHttpRoute(
            name: "getfromwallet",
            routeTemplate: "api/withdraw/getfromwallet",
            defaults: new { controller = "Webapi", action = "Withdraw_GetFromWallet" }
         );
            config.Routes.MapHttpRoute(
               name: "Deposit_Historys",
               routeTemplate: "api/deposit/history",
               defaults: new { controller = "Webapi", action = "Deposit_Historys" }
            );
            config.Routes.MapHttpRoute(
              name: "Deposit_Getby_Symbol",
              routeTemplate: "api/deposit/wallet",
              defaults: new { controller = "Webapi", action = "Deposit_Getby_Symbol" }
           );

            config.Routes.MapHttpRoute(
                        name: "AppGetUserWalletCopytrade",
                        routeTemplate: "api/user/user_wallet_copytrade",
                        defaults: new { controller = "Webapi", action = "User_WalletAddress_CopyTrade_GetByUserName" }
                  );

            config.Routes.MapHttpRoute(
                      name: "Get_WalletAddressUSD_ByUser",
                      routeTemplate: "api/user/get-walletaddress-usd",
                      defaults: new { controller = "Webapi", action = "Get_WalletAddressUSD_ByUser" }
                    );


            config.Routes.MapHttpRoute(
                  name: "Transfer_USD_By_WalletAddress",
                  routeTemplate: "api/user/transfer_money",
                  defaults: new { controller = "Webapi", action = "Transfer_USD_GetByWalletAddress" }
            );

            config.Routes.MapHttpRoute(
              name: "Transfer_History",
              routeTemplate: "api/user/transfer_history",
              defaults: new { controller = "Webapi", action = "Transfer_History" }
              );

            config.Routes.MapHttpRoute(
                       name: "API_BuyMasterIB",
                       routeTemplate: "api/user/buy_ib",
                       defaults: new { controller = "Webapi", action = "BuyMasterIB" }
                 );

            config.Routes.MapHttpRoute(
                        name: "Get_UserProfile",
                        routeTemplate: "api/user/get_userprofile",
                        defaults: new { controller = "Webapi", action = "Get_UserProfile" }
             );
            config.Routes.MapHttpRoute(
                        name: "UpdateProfile",
                        routeTemplate: "api/user/updateprofile",
                        defaults: new { controller = "Webapi", action = "UpdateProfile" }
             );

            config.Routes.MapHttpRoute(
                        name: "UserGetDasboard",
                        routeTemplate: "api/dashboard/get",
                        defaults: new { controller = "Webapi", action = "GetDasboard" }
             );

            config.Routes.MapHttpRoute(
                        name: "GetAffiliateStatistic",
                        routeTemplate: "api/affiliate/statistic",
                        defaults: new { controller = "Webapi", action = "GetAffiliateStatistic" }
             );

            config.Routes.MapHttpRoute(
                        name: "GetNetworkStatistic",
                        routeTemplate: "api/network/statistic",
                        defaults: new { controller = "Webapi", action = "GetNetworkStatistic" }
             );

            config.Routes.MapHttpRoute(
                        name: "GetProfitStatistic",
                        routeTemplate: "api/profit/statistic",
                        defaults: new { controller = "Webapi", action = "GetProfitStatistic" }
             );

            config.Routes.MapHttpRoute(
                        name: "GetLevelNetworkStatistic",
                        routeTemplate: "api/network/count_level",
                        defaults: new { controller = "Webapi", action = "GetLevelNetworkStatistic" }
             );

            //config.Routes.MapHttpRoute(
            //            name: "AffiliateTradingHistory",
            //            routeTemplate: "api/user/affiliateGets",
            //            defaults: new { controller = "Webapi", action = "AffiliateTradingHistory" }
            // );

            config.Routes.MapHttpRoute(
                        name: "AffiliateAgencyHistory",
                        routeTemplate: "api/affiliate/agencyhistory",
                        defaults: new { controller = "Webapi", action = "AffiliateAgencyHistory" }
             );

            config.Routes.MapHttpRoute(
                        name: "AffiliateChartMembers",
                        routeTemplate: "api/affiliate/chart_members",
                        defaults: new { controller = "Webapi", action = "AffiliateChartMembers" }
             );

            config.Routes.MapHttpRoute(
                        name: "AffiliateChartAgencyCom",
                        routeTemplate: "api/affiliate/chart_agency_com",
                        defaults: new { controller = "Webapi", action = "AffiliateChartAgencyCom" }
             );
            config.Routes.MapHttpRoute(
             name: "TOOL_INVEST_PACKAGES",
             routeTemplate: "api/user/toolInvestAuto",
             defaults: new { controller = "Webapi", action = "TOOL_INVEST_PACKAGES" }
          );

            #region api old
            config.Routes.MapHttpRoute(
                name: "MarketPrice",
                routeTemplate: "api/martkets",
                defaults: new { controller = "Webapi", action = "MarketPrice" }
            );

            config.Routes.MapHttpRoute(
               name: "Candlestick",
               routeTemplate: "api/candlestick",
               defaults: new { controller = "Webapi", action = "Candlestick" }
            );
            config.Routes.MapHttpRoute(
               name: "TradePairs",
               routeTemplate: "api/trade_pairs",
               defaults: new { controller = "Webapi", action = "TradePairs" }
            );
            config.Routes.MapHttpRoute(
              name: "Investment",
              routeTemplate: "api/user/investment",
              defaults: new { controller = "Webapi", action = "Investment" }
           );
            config.Routes.MapHttpRoute(
              name: "InvestmentHistory",
              routeTemplate: "api/user/investmenthistory",
              defaults: new { controller = "Webapi", action = "InvestmentHistory" }
           );
            config.Routes.MapHttpRoute(
             name: "Get_PackageInvestments",
             routeTemplate: "api/package/gets",
             defaults: new { controller = "Webapi", action = "Get_PackageInvestments" }
          );
            config.Routes.MapHttpRoute(
             name: "ActiveEmailRegister",
             routeTemplate: "api/user/active-email",
             defaults: new { controller = "Webapi", action = "ActiveEmailRegister" }
          );
           
            config.Routes.MapHttpRoute(
              name: "TransferConfirm",
              routeTemplate: "api/user/transfer_confirm",
              defaults: new { controller = "Webapi", action = "TransferConfirm" }
           );
            config.Routes.MapHttpRoute(
              name: "TradingLastResults",
              routeTemplate: "api/trading/lastresult",
              defaults: new { controller = "Webapi", action = "TradingLastResults" }
           );
            config.Routes.MapHttpRoute(
              name: "User_GetProfile",
              routeTemplate: "api/user/getProfile",
              defaults: new { controller = "Webapi", action = "GetProfile" }
          );

            config.Routes.MapHttpRoute(
                name: "User_GetFaCode",
                routeTemplate: "api/user/twofacode/get",
                defaults: new { controller = "Webapi", action = "GetFaCode" }
            );

            config.Routes.MapHttpRoute(
                name: "User_SetupFaCode",
                routeTemplate: "api/user/twofacode/update",
                defaults: new { controller = "Webapi", action = "SetupFaCode" }
            );
            #endregion
            config.Routes.MapHttpRoute(
              name: "Withdraw_SendCodeEmail",
              routeTemplate: "api/Withdraw/SendCodeEmail",
              defaults: new { controller = "Webapi", action = "Withdraw_SendCodeEmail" }
          );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
