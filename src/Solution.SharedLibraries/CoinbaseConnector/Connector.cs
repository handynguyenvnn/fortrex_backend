using System;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using System.IO;
using System.Configuration;
using RestSharp;
using CoinbaseConnector.ModelCoin.Base;
using Newtonsoft.Json;

namespace CoinbaseConnector
{
	// For full documentation on all Coinbase API calls, please visit https://coinbase.com/api/doc

	// Disclaimer: I do not work for Coinbase.com, but I will attempt to answer any  
	// questions you may have about THIS wrapper (not about Coinbase) if you post them 
	// to my GitHub repo: http://www.github.com/chrisgwilliams/coinbase.NET or message 
	// me via Twitter: @chrisgwilliams 
	
	public class Connector
    {
        //private string API_KEY = ConfigurationManager.AppSettings["API_KEY"]; //APIKeys.API_KEY;
        //private string API_SECRET = ConfigurationManager.AppSettings["API_SECRET"]; //APIKeys.API_SECRET;
        //private string AUTHORIZATION = ConfigurationManager.AppSettings["AUTHORIZATION"]; //APIKeys.AUTHORIZATION;
        //private string VERSION = "2017-05-19";
        private string URL_BASE = "https://api.coinbase.com/v2/";
		private const string GET = "GET";
		private const string POST = "POST";
		private const string PUT = "PUT";
		private const string DELETE = "DELETE";

        #region remove
        // Account Changes
        //public string GetAccountChanges(int page = 1)
        //{
        //	// Page field is optional. Default is 1
        //	return JsonRequest(URL_BASE + "account_changes?page=" + page, GET);
        //}

        //// Account - DEPRECATED
        //public string GetAccountBalance()
        //{
        //	return JsonRequest(URL_BASE + "account/balance", GET);
        //}
        //public string GetCurrentReceiveAddress()
        //{
        //	return JsonRequest(URL_BASE + "account/receive_address", GET);
        //}
        //public string GenerateReceiveAddress()
        //{
        //	return JsonRequest(URL_BASE + "account/generate_receive_address", POST);
        //}
        //public string GenerateReceiveAddress(String callbackURL, String label)
        //{
        //	return JsonRequest(URL_BASE + "account/generate_receive_address?address[callback_url]=" + callbackURL+"&address[label]=" + label, POST); 
        //}

        //// Accounts - NEW
        //public string GetUserAccounts(int page = 1, int limit = 25, Boolean all_accounts = false)
        //{
        //	var sb = new StringBuilder();

        //	sb.Append("?page=" + page);

        //	if (limit > 1000) limit = 1000;
        //	sb.Append("&limit=" + limit);

        //	sb.Append("&all_accounts=" + all_accounts.ToString());

        //	return JsonRequest(URL_BASE + "accounts" + sb.ToString(), GET);
        //}
        //public string GetUserAccountBalance(string id)
        //{
        //	return JsonRequest(URL_BASE + "accounts/" + id + "/balance", GET);
        //}
        //public string CreateAccount(string account_name) 
        //{
        //	return JsonRequest(URL_BASE + "accounts?account[name]=" + account_name, POST);
        //}
        //public string UpdateAccountSettings(string id)
        //{
        //	return JsonRequest(URL_BASE + "accounts/" + id, PUT);
        //}
        //public string SetAccountPrimary(string id)
        //{
        //	return JsonRequest(URL_BASE + "accounts/" + id + "/primary", POST);
        //}
        //public string DeleteAccount(string id)
        //{
        //	return JsonRequest(URL_BASE + "accounts/" + id, DELETE);
        //}

        //// Addresses
        //public string GetAddressList(int page = 1, int limit = 25, String query = "")
        //{
        //	// Page field is optional. Default is 1
        //	return JsonRequest(URL_BASE + "addresses?page=" + page + "&limit=" + limit + "&query=" + query, GET);
        //}

        //// OAuth Applications
        //public string GetOAuthApplicationsList(int page = 1)
        //{
        //	return JsonRequest(URL_BASE + "oauth/applications?page=" + page, GET);
        //}
        //public string GetOauthApplicationByID(String ID)
        //{
        //	return JsonRequest(URL_BASE + "oauth/applications/" + ID, GET);
        //}
        //public string CreateOAuthApplication(String name, String redirectURI)
        //{
        //	return JsonRequest(URL_BASE + "oauth/applications?application[name]=" + name + "&application[redirect_uri]=" + redirectURI, POST);
        //}

        //// Authorization
        //public string GetApplicationAccountAccessInfo()
        //{
        //	return JsonRequest(URL_BASE + "authorization", GET);
        //}

        //// Buttons
        //public string CreatePaymentButton(String name, String price, String currency, String type = "buy_now", 
        //								  String repeat = "never", String style = "buy_now_large", String text = "Pay With Bitcoin",
        //								  String description = "", String custom = "", Boolean custom_secure = false,
        //								  String callback_url = "", String success_url = "", String cancel_url = "", 
        //								  String info_url = "", Boolean auto_redirect = true, Boolean variable_price = false,
        //								  Boolean choose_price = false, Boolean include_address = true, Boolean include_email = true,
        //								  String price1 = "", String price2 = "", String price3 = "", String price4 = "", 
        //								  String price5 = "")
        //{
        //	var sb = new StringBuilder();

        //	// REQUIRED PARAMS
        //	sb.Append("?button[name]=" + name);
        //	// Can be more then two significant digits if price_currency_iso equals BTC
        //	if (currency != "BTC") string.Format("{0:0.00}", price);
        //	sb.Append("&button[price_string]=" + price);
        //	// Price currency as ISO 4217 Currency Code (i.e. USD, BTC)
        //	sb.Append("&button[price_currency_iso]=" + currency);

        //	// OPTIONAL PARAMS
        //	// Type must be one of buy_now, donation, or subscription. Default is buy_now
        //	if (type != "") sb.Append("&button[type]=" + type);
        //	// Style must be one of buy_now_large, buy_now_small, donation_large, donation_small, 
        //	// subscription_large, subscription_small, custom_large, custom_small, and none. Default is buy_now_large
        //	if (style != "") sb.Append("&button[style]=" + style);
        //	// Text may be used on custom_large or custom_small styles (above.) Default is "Pay With Bitcoin."
        //	if (text != "") sb.Append("&button[text]=" + text);
        //	// Description may be used to add more infomation to transaction notes
        //	if (description != "") sb.Append("&button[description]=" + description);
        //	// Custom usually represents an Order, User or Product ID corresponding to a record in your database.
        //	if (custom != "") sb.Append("&button[custom]=" + custom);
        //	// Custom Secure should be set to TRUE to prevent the custom parameter from being viewed or modified after 
        //	// the button has been created. Use this if you are storing sensitive data in the custom parameter which you 
        //	// don’t want to be faked or leaked to the end user. Defaults to FALSE.
        //	if (custom_secure != false) sb.Append("&button[custom_secure]=" + custom_secure);
        //	// A custom callback URL specific to this button. It will receive the same information that would otherwise 
        //	// be sent to your Instant Payment Notification URL. If you have an Instant Payment Notification URL set on 
        //	// your account, this will be called instead — they will not both be called.
        //	if (callback_url != "") sb.Append("&button[callback_url]=" + callback_url);
        //	// A custom success URL specific to this button. The user will be redirected to this URL after a successful 
        //	// payment. It will receive the same parameters that would otherwise be sent to the default success url set
        //	// on the account.
        //	if (success_url != "") sb.Append("&button[success_url]=" + success_url);
        //	// A custom cancel URL specific to this button. The user will be redirected to this URL after a canceled 
        //	// order. It will receive the same parameters that would otherwise be sent to the default cancel url set 
        //	// on the account.
        //	if (cancel_url != "") sb.Append("&button[cancel_url]=" + cancel_url);
        //	// A custom info URL specific to this button. Displayed to the user after a successful purchase for sharing.
        //	// It will receive the same parameters that would otherwise be sent to the default info url set on the account.
        //	if (info_url != "") sb.Append("&button[info_url]=" + info_url);
        //	// Auto-redirect users to success or cancel url after payment. (Cancel url if the user pays the wrong amount.)
        //	// Default is TRUE
        //	if (auto_redirect != true) sb.Append("&button[auto_redirect]=" + auto_redirect);
        //	// Allow users to change the price on the generated button. Default is FALSE
        //	if (variable_price != false) sb.Append("&button[variable_price]=" + variable_price);
        //	// Show some suggested prices. Default is FALSE
        //	if (choose_price != false) sb.Append("&button[choose_price]=" + choose_price);
        //	// Collect shipping address from customer (not for use with inline iframes). Default is TRUE
        //	if (include_address != true) sb.Append("&button[include_address]=" + include_address);
        //	// Collect email address from customer (not for use with inline iframes). Default is TRUE
        //	if (include_email != true) sb.Append("&button[include_email]=" + include_email);
        //	// Suggested price 1
        //	if (price1 != "") sb.Append("&button[price1]=" + price1);
        //	// Suggested price 2
        //	if (price2 != "") sb.Append("&button[price2]=" + price2);
        //	// Suggested price 3
        //	if (price3 != "") sb.Append("&button[price3]=" + price3);
        //	// Suggested price 4
        //	if (price4 != "") sb.Append("&button[price4]=" + price4);
        //	// Suggested price 5
        //	if (price5 != "") sb.Append("&button[price5]=" + price5);

        //	// CONDITIONAL PARAMS
        //	// Repeat must be one of never, daily, weekly, every_two_weeks, monthly, quarterly, or yearly. 
        //	// Required if type = subscription. Default value is never.
        //	sb.Append("&button[repeat]=" + repeat);

        //	return JsonRequest(URL_BASE + "buttons" + sb.ToString(), POST);
        //}
        //public string CreateOrderForButton(String code)
        //{
        //	return JsonRequest(URL_BASE + "buttons/" + code + "/create_order", POST);
        //}

        //// Buys
        //public string PurchaseBitcoin(float qty, Boolean agree_btc_amount_varies = false, String payment_method_id = "")
        //{
        //	// The agree_btc_amount_varies parameter is optional and indicates whether or not the buyer would still like
        //	// to buy if they have to wait for their money to arrive to lock in a price. Default value is FALSE
        //	return JsonRequest(URL_BASE + "buys?qty=" + qty + "&agree_btc_amount_varies=" + agree_btc_amount_varies 
        //		+ "&payment_method_id=" + payment_method_id, POST);
        //}

        //// Contacts
        //public string GetEmailContactsList(int page = 1, int limit = 25, String query = "")
        //{

        //	var sb = new StringBuilder();

        //	sb.Append("?page=" + page);

        //	if (limit > 1000) limit = 1000;
        //	sb.Append("&limit=" + limit);

        //	if (query != "") sb.Append("&query=" + query);

        //	return JsonRequest(URL_BASE + "contacts" + sb.ToString(), GET);
        //}

        //// Currencies
        //public string GetSupportedCurrenciesList()
        //{
        //	return JsonRequest(URL_BASE + "currencies", GET);
        //}
        //public string GetBTCExchangeRate()
        //{
        //	return JsonRequest(URL_BASE + "currencies/exchange_rates", GET);
        //}

        //// Orders
        //public string GetReceivedMerchantOrdersList(int page = 1)
        //{
        //	// Page field is optional. Default is 1
        //	return JsonRequest(URL_BASE + "orders?page=" + page, GET);
        //}
        //// Use this endpoint to create a one-time unique order that does not use the Coinbase merchant tools.
        //// Ex: Generating a bitcoin address for an order and displaying it directly in your page, to only one user.
        //public string CreateNewOrder(String name, String price, String currency, String type = "buy_now",
        //							 String repeat = "never", String style = "buy_now_large", String text = "Pay With Bitcoin",
        //							 String description = "", String custom = "", Boolean custom_secure = false,
        //							 String callback_url = "", String success_url = "", String cancel_url = "",
        //							 String info_url = "", Boolean auto_redirect = true, Boolean variable_price = false,
        //							 Boolean choose_price = false, Boolean include_address = true, Boolean include_email = true,
        //							 String price1 = "", String price2 = "", String price3 = "", String price4 = "",
        //							 String price5 = "")
        //{
        //	var sb = new StringBuilder();

        //	// REQUIRED PARAMS
        //	sb.Append("?button[name]=" + name);
        //	// Can be more then two significant digits if price_currency_iso equals BTC
        //	if (currency != "BTC") string.Format("{0:0.00}", price);
        //	sb.Append("&button[price_string]=" + price.ToString());
        //	// Price currency as ISO 4217 Currency Code (i.e. USD, BTC)
        //	sb.Append("&button[price_currency_iso]=" + currency);

        //	// OPTIONAL PARAMS
        //	// Type must be one of buy_now, donation, or subscription. Default is buy_now
        //	if (type != "") sb.Append("&button[type]=" + type);
        //	// Style must be one of buy_now_large, buy_now_small, donation_large, donation_small, 
        //	// subscription_large, subscription_small, custom_large, custom_small, and none. Default is buy_now_large
        //	if (style != "") sb.Append("&button[style]=" + style);
        //	// Text may be used on custom_large or custom_small styles (above.) Default is "Pay With Bitcoin."
        //	if (text != "") sb.Append("&button[text]=" + text);
        //	// Description may be used to add more infomation to transaction notes
        //	if (description != "") sb.Append("&button[description]=" + description);
        //	// Custom usually represents an Order, User or Product ID corresponding to a record in your database.
        //	if (custom != "") sb.Append("&button[custom]=" + custom);
        //	// Custom Secure should be set to TRUE to prevent the custom parameter from being viewed or modified after 
        //	// the button has been created. Use this if you are storing sensitive data in the custom parameter which you 
        //	// don’t want to be faked or leaked to the end user. Defaults to FALSE.
        //	if (custom_secure != false) sb.Append("&button[custom_secure]=" + custom_secure);
        //	// A custom callback URL specific to this button. It will receive the same information that would otherwise 
        //	// be sent to your Instant Payment Notification URL. If you have an Instant Payment Notification URL set on 
        //	// your account, this will be called instead — they will not both be called.
        //	if (callback_url != "") sb.Append("&button[callback_url]=" + callback_url);
        //	// A custom success URL specific to this button. The user will be redirected to this URL after a successful 
        //	// payment. It will receive the same parameters that would otherwise be sent to the default success url set
        //	// on the account.
        //	if (success_url != "") sb.Append("&button[success_url]=" + success_url);
        //	// A custom cancel URL specific to this button. The user will be redirected to this URL after a canceled 
        //	// order. It will receive the same parameters that would otherwise be sent to the default cancel url set 
        //	// on the account.
        //	if (cancel_url != "") sb.Append("&button[cancel_url]=" + cancel_url);
        //	// A custom info URL specific to this button. Displayed to the user after a successful purchase for sharing.
        //	// It will receive the same parameters that would otherwise be sent to the default info url set on the account.
        //	if (info_url != "") sb.Append("&button[info_url]=" + info_url);
        //	// Auto-redirect users to success or cancel url after payment. (Cancel url if the user pays the wrong amount.)
        //	// Default is TRUE
        //	if (auto_redirect != true) sb.Append("&button[auto_redirect]=" + auto_redirect);
        //	// Allow users to change the price on the generated button. Default is FALSE
        //	if (variable_price != false) sb.Append("&button[variable_price]=" + variable_price);
        //	// Show some suggested prices. Default is FALSE
        //	if (choose_price != false) sb.Append("&button[choose_price]=" + choose_price);
        //	// Collect shipping address from customer (not for use with inline iframes). Default is TRUE
        //	if (include_address != true) sb.Append("&button[include_address]=" + include_address);
        //	// Collect email address from customer (not for use with inline iframes). Default is TRUE
        //	if (include_email != true) sb.Append("&button[include_email]=" + include_email);
        //	// Suggested price 1
        //	if (price1 != "") sb.Append("&button[price1]=" + price1);
        //	// Suggested price 2
        //	if (price2 != "") sb.Append("&button[price2]=" + price2);
        //	// Suggested price 3
        //	if (price3 != "") sb.Append("&button[price3]=" + price3);
        //	// Suggested price 4
        //	if (price4 != "") sb.Append("&button[price4]=" + price4);
        //	// Suggested price 5
        //	if (price5 != "") sb.Append("&button[price5]=" + price5);

        //	// CONDITIONAL PARAMS
        //	// Repeat must be one of never, daily, weekly, every_two_weeks, monthly, quarterly, or yearly. 
        //	// Required if type = subscription. Default value is never.
        //	sb.Append("&button[repeat]=" + repeat);

        //	return JsonRequest(URL_BASE + "orders" + sb.ToString(), POST);
        //}
        //// ID can represent an actual Order ID or a custom merchant field.
        //public string GetMerchantOrderByID(string ID = "")
        //{
        //	return JsonRequest(URL_BASE + "orders/" + ID, GET);
        //}

        //// Payment Methods
        //public string GetAssociatedPaymentMethods()
        //{
        //	return JsonRequest(URL_BASE + "payment_methods", GET);
        //}

        //// Prices 
        //public string GetTotalBuyPriceForBitcoin(float qty = 1, String currency = "USD")
        //{
        //	// qty is optional. Default value is 1
        //	// currency is optional. Default value is USD (this is the only supported value at this time.)
        //	return JsonRequest(URL_BASE + "prices/buy?qty=" + qty + "&currency=" + currency, GET);
        //}
        //public string GetTotalSellPriceForBitcoin(float qty = 1, String currency = "USD")
        //{
        //	// qty is optional. Default value is 1.
        //	// currency is optional. Default value is USD (this is the only supported value at this time.)
        //	return JsonRequest(URL_BASE + "prices/sell?qty=" + qty + "&currency=" + currency, GET);
        //}
        //public string GetSpotPriceForBitcoin(String currency = "USD")
        //{
        //	// Currency must be an ISO 4217 Currency Code. Default is USD
        //	return JsonRequest(URL_BASE + "prices/spot_rate?currency=" + currency, GET);
        //}
        //public string GetHistoricalSpotPriceForBitcoin(int page = 1)
        //{
        //	// Page field is optional. Default is 1
        //	return JsonRequest(URL_BASE + "prices/historical?page=" + page, GET);
        //}

        //// Recurring Payments
        //public string GetRecurringPaymentsList(String ID = "", int page = 1, int limit = 25)
        //{
        //	// ID field is optional. Default is no parameter. 
        //	// If you specify an ID, you get an individual recurring payment, otherwise you get a list
        //	if (ID != "") return JsonRequest(URL_BASE + "recurring_payments/" + ID, GET);

        //	return JsonRequest(URL_BASE + "recurring_payments?page=" + page + "&limit=" + limit, GET);
        //}

        //// Reports
        //public string GetReportsList()
        //{
        //	return JsonRequest(URL_BASE + "reports", GET);
        //}
        //public string GenerateCSVReport(String email, String type, String timeRange = "", String timeRangeStart = "", String timeRangeEnd = "",  
        //	String callbackURL = "", String startType = "now", String nextRunDate = "", String nextRunTime = "", String repeat = "", String times = "", 
        //	String accountID = "")
        //{
        //	// Valid values for type: transactions, orders, transfers
        //	// Valid values for time_range: today, yesterday, past_7, past_30, month_to_date, last_full_month, year_to_date, 
        //	// last_full_year, all, custom.  If custom, must supply time_range_start and time_range_end. Defaults to past_30
        //	// Valid values for start_type: now, on
        //	// Valid values for repeat: never, daily, weekly, every_two_weeks, monthly, quarterly, yearly. Defaults to never.
        //	// If an email address is not provided, address on account will be used.
        //	var sb = new StringBuilder();

        //	// REQUIRED PARAMS
        //	sb.Append("?report[type]=" + type);
        //	sb.Append("&report[email]=" + email);			

        //	// OPTIONAL PARAMS
        //	if (timeRange != "") sb.Append("&report[time_range]=" + timeRange);
        //	if (callbackURL != "") sb.Append("&report[callback_url]=" + callbackURL);
        //	if (startType != "") sb.Append("&report[start_type]=" + startType);
        //	if (repeat != "") sb.Append("&report[repeat]=" + repeat);
        //	if (accountID != "") sb.Append("&report[account_id]=" + accountID);

        //	// CONDITIONAL PARAMS
        //	if (timeRange == "custom") sb.Append("&report[time_range_start]=" + timeRangeStart + "&report[time_range_end]=" + timeRangeEnd);
        //	if (startType == "on") sb.Append("&report[next_run_date]=" + nextRunDate + "&report[next_run_time]=" + nextRunTime);
        //	if (repeat != "never") 
        //		if (repeat != "") 
        //			if (times != "") sb.Append("&report[times]=" + times);

        //	return JsonRequest(URL_BASE + "reports" + sb.ToString(), POST);
        //}
        //public string GetReportByID(string ID)
        //{
        //	return JsonRequest(URL_BASE + "reports" + "/" + ID, GET);
        //}

        //// Sells
        //public string SellBitcoin(float qty, String payment_method_id = "")
        //{
        //	// Quantity of Bitcoin to sell is required.
        //	// Payment Method ID is optional. Will use default account ID. Must have verified bank account to work.
        //	return JsonRequest(URL_BASE + "sells?qty=" + qty + "&payment_method_id=" + payment_method_id, POST);	
        //}

        //// Subscribers
        //public string GetSubscribersList(String ID = "", int page = 1, int limit = 25)
        //{
        //	// ID field is optional. Default is no parameter. 
        //	// If you specify an ID, you get an individual customer subscription, otherwise you get a list
        //	if (ID != "") return JsonRequest(URL_BASE + "subscribers/" + ID, GET);

        //	return JsonRequest(URL_BASE + "subscribers?page=" + page + "&limit=" + limit, GET);
        //}

        //// Tokens
        //public string CreateToken()
        //{
        //	// This call creates a token redeemable for Bitcoin. Returned Bitcoin address can be used to send money 
        //	// to the token, and will be credited to the account of the token redeemer if money is sent.
        //	return JsonRequest(URL_BASE + "tokens", POST);
        //}
        //public string RedeemToken(String tokenID)
        //{
        //	// This call claims a redeemable token for its address and bitcoin(s).
        //	return JsonRequest(URL_BASE + "tokens/redeem?token_id=" + tokenID, POST);
        //}
        #endregion
        //// Transactions

        public string GetTransactionsList(string url = "", string accountId = "", int page = 1, int limit = 25)
        {
            string urlBase = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                urlBase = URL_BASE.TrimEnd('/') + url;
            }
            else
            {
                urlBase = URL_BASE + "accounts/" + accountId + "/transactions";
            }

            return JsonRequest(EnumMethod.BTC, urlBase, GET);
        }

        public string GetTransactionsListETH(string url = "", string accountId = "", int page = 1, int limit = 25)
        {
            string urlBase = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                urlBase = URL_BASE.TrimEnd('/') + url;
            }
            else
            {
                urlBase = URL_BASE + "accounts/" + accountId + "/transactions";
            }

            return JsonRequest(EnumMethod.ETH, urlBase, GET);
        }

        public string GetTransactionsDetail(string accountId, string transactonId)
        {
            // ID field is optional. Default is no parameter. 
            // If you specify an ID, you get an individual transaction, otherwise you get a list
            string urlBase = urlBase = URL_BASE + "accounts/" + accountId + "/transactions/" + transactonId;
            return JsonRequest(EnumMethod.BTC, urlBase, GET);
        }

        public string GetTransactionsDetailETH(string accountId, string transactonId)
        {
            // ID field is optional. Default is no parameter. 
            // If you specify an ID, you get an individual transaction, otherwise you get a list
            string urlBase = urlBase = URL_BASE + "accounts/" + accountId + "/transactions/" + transactonId;
            return JsonRequest(EnumMethod.ETH, urlBase, GET);
        }

        #region remove
        //      public string SendMoney(string account, string type, string to, string amount, string currency)
        //{
        //          string body = "";// string.Format("\"type\":\"{0}\",\"to\": \"{1}\",\"amount\": \"{2}\",\"currency\": \"{3}\"", type, to, amount, currency);
        //          string param = string.Format("?type={0}&to={1}&amount={2}&currency={3}", type, to, amount, currency);

        //          return JsonRequest(URL_BASE + string.Format("accounts/{0}/transactions{1}", account, param), POST, body);
        //}
        //public string SendInvoice(String from, String amount = "", String amountString = "", String amountCurrencyISO = "",
        //						  String notes = "", String accountID = "")
        //{
        //	// This lets the user request money from a bitcoin address. If you pass an amount param it will be 
        //	// interpreted as a bitcoin amount. Alternatively you can pass an amount_string and amount_currency_iso 
        //	// such as ‘USD’ or ‘EUR’ and the equivalent amount of bitcoin will be sent at current exchange rates.
        //	var sb = new StringBuilder();

        //	// REQUIRED PARAMS
        //	sb.Append("?transaction[from]=" + from);

        //	// CONDITIONAL PARAMS
        //	// If you supply values for amount, amount_string AND amount_currency_iso, then amount takes precedence.
        //	if (amount != "")
        //	{
        //		sb.Append("&transaction[amount]=" + amount);
        //	}
        //	else
        //	{
        //		sb.Append("&transaction[amount_string]=" + amountString);
        //		sb.Append("&transaction[amount_currency_iso]=" + amountCurrencyISO);
        //	}

        //	// OPTIONAL PARAMS
        //	if (notes != "") sb.Append("&transaction[notes]=" + notes);
        //	if (accountID != "") sb.Append("&transaction[account_id]=" + accountID);

        //	return JsonRequest(URL_BASE + "transactions/request_money" + sb.ToString(), POST);
        //}
        //public string ResendInvoice(String ID)
        //{
        //	// This lets the user resend a money request.
        //	return JsonRequest(URL_BASE + "transactions/" + ID + "/resend_request", PUT);
        //}
        //public string CancelMoneyRequest(String ID)
        //{
        //	// This lets a user cancel a money request. Money requests can be canceled by the sender or the recipient.
        //	return JsonRequest(URL_BASE + "transactions/" + ID + "/cancel_request", DELETE);
        //}
        //public string CompleteMoneyRequest(String ID)
        //{
        //	// This lets a user complete a money request. Money requests can only be completed by the sender (not the 
        //	// recipient.) The sender in this context is the user who is sending money (not sending the invoice.)
        //	return JsonRequest(URL_BASE + "transactions/" + ID + "/complete_request", PUT);
        //}

        //// Transfers
        //public string GetTransfersList(int page = 1, int limit = 25)
        //{
        //	// This returns the user's bitcoin purchases and sells. Sorted by created_at, descending.
        //	// page param is optional, default value is 1.
        //	// limit param is optional, default value is 25, max value is 1000.
        //	if (limit > 1000) limit = 1000;
        //	return JsonRequest(URL_BASE + "transfers?page=" + page + "&limit=" + limit, GET);
        //}

        //// Users
        //public string CreateNewUser(String email, String password, String referrerID = "", String clientID = "")
        //{
        //	// This method creates a user with an email and password. The receive address for the user is returned 
        //	// as well if you’d like to send a first payment to them. To generate additional receive addresses you 
        //	// will need to be authenticated as this user.

        //	// This method is useful if you would only like to create the user, or would like to send to their 
        //	// bitcoin address instead of an email address.

        //	var sb = new StringBuilder();

        //	// REQUIRED PARAMS
        //	sb.Append("?user[email]=" + email);
        //	// A strong password - at least eight digits without dictionary words.
        //	sb.Append("&user[password]=" + password);

        //	// OPTIONAL PARAMS
        //	if (referrerID != "") sb.Append("&user[referrer_id]=" + referrerID);
        //	if (clientID != "") sb.Append("&user[client_id]=" + clientID);

        //	return JsonRequest(URL_BASE + "users" + sb.ToString(), POST);

        //}
        #endregion

        public string GetAccountSettings()
		{
			// Show current user with account settings.
			return JsonRequest(EnumMethod.BTC, URL_BASE + "accounts", GET);
		}

        public string GetAccountSettingsETH()
        {
            // Show current user with account settings.
            return JsonRequest(EnumMethod.ETH, URL_BASE + "accounts", GET);
        }

  //      public string UpdateAccountSettings(String id, String name = "", String email = "", String pin = "", 
		//									String nativeCurrency = "", String timeZone = "")
		//{
		//	// This lets you update account settings for the current user. Only these fields are updatable.
		//	var sb = new StringBuilder();

		//	sb.Append("&id=" + id);
		//	if (name != "") sb.Append("&user[name]=" + name);
		//	if (email != "") sb.Append("&user[email]=" + email);
		//	if (pin != "") sb.Append("&user[pin]=" + pin);
		//	if (nativeCurrency != "") sb.Append("&user[native_currency]=" + nativeCurrency);
		//	if (timeZone != "") sb.Append("&user[time_zone]=" + timeZone);

		//	return JsonRequest(URL_BASE + "users/" + id + sb.ToString(), PUT);
		//}

        public string CreateAddress(string userId, string name)
        {
            string url = string.Format("{0}accounts/{1}/addresses", URL_BASE, userId);
            string body = "{\"name\": \"" + name + "\"}";
            return JsonRequest(EnumMethod.BTC, url, POST, body);
        }

        public string CreateAddressETH(string userId, string name)
        {
            string url = string.Format("{0}accounts/{1}/addresses", URL_BASE, userId);
            string body = "{\"name\": \"" + name + "\"}";
            return JsonRequest(EnumMethod.ETH, url, POST, body);
        }

        private string JsonDataAuthorization(string url, string method)
        {
            url = Uri.EscapeUriString(url);

            string returnData = string.Empty;

            var webRequest = System.Net.WebRequest.Create(url);
            if (webRequest != null)
            {
                webRequest.Method = method;
                webRequest.ContentType = "application/json";
                //webRequest.Headers.Add("Authorization", "Bearer " + AUTHORIZATION);
                webRequest.Headers.Add("CB-VERSION", "2017-05-19");
                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        returnData = sr.ReadToEnd();
                    }
                }
            }

            return returnData;
        }

        private string JsonRequest(EnumMethod methodPayment, string url, string method, string body = "")
		{
            bool? WriteLogDetails = bool.Parse(ConfigurationManager.AppSettings["WriteLogDetails"]);
            string returnData = string.Empty;
            url = Uri.EscapeUriString(url);
            var typeMethod = Method.GET;
            switch(method)
            {
                case PUT:
                    typeMethod = Method.PUT;
                    break;
                case POST:
                    typeMethod = Method.POST;
                    break;
                case DELETE:
                    typeMethod = Method.DELETE;
                    break;
                case GET:
                default:
                    typeMethod = Method.GET;
                    break;
            }
            //int second = 0;
            //while (second < 30000)
            //{
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 |
                                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var client = new RestClient(url);
                var request = new RestRequest(typeMethod);
                request.AddHeader("Content-Type", "application/json");

                if (!string.IsNullOrEmpty(body))
                {
                    request.AddJsonBody(body);
                }

                if (methodPayment == EnumMethod.BTC)
                {
                    HelperCoin.ExcuteRestRequest(client, request);
                }
                else if (methodPayment == EnumMethod.ETH)
                {
                    HelperCoin.ExcuteRestRequestETH(client, request);
                }
                //Thread.Sleep(second);
                var response = client.Execute(request);
                Thread.Sleep(2);
                if (WriteLogDetails ?? false)
                {
                    LibraryLog.WriteErrorLog(DateTime.Now.ToLocalTime().ToString() + "- response.StatusCode: " + response.StatusCode);
                    LibraryLog.WriteErrorLog(string.Format("url: {0}, body: {1}", url, body));
                    Console.WriteLine("response: " + JsonConvert.SerializeObject(response));
                    Console.WriteLine("(string)response.ResponseStatus: " + response.ResponseStatus.ToString());
                }
                if ((int)response.ResponseStatus == 1 && ((int)response.StatusCode == 200 || (int)response.StatusCode == 201))
                {
                    //LibraryLog.WriteErrorLog("second success: " + second.ToString());
                    if (!string.IsNullOrEmpty(response.Content.Trim()))
                    {
                        returnData = response.Content.Trim();
                    }
                    //break;
                }
                //LibraryLog.WriteErrorLog("second fail: " + second.ToString());
            }
            catch
            {
                //break;
            }
                //second += 500;
            //}
            //LibraryLog.WriteErrorLog("---------------------------------------");
            return returnData;
		}

		private static byte[] StringEncode(string text)
		{
			var encoding = new ASCIIEncoding();
			return encoding.GetBytes(text);
		}

		private static string HashEncode(byte[] hash)
		{
			return BitConverter.ToString(hash).Replace("-", "").ToLower();
		}

		private static byte[] HashHMAC(byte[] key, byte[] message)
		{
			var hash = new HMACSHA256(key);
			return hash.ComputeHash(message);
		}

        private static string HashHMACAuth(string requestHashString, string secretKey)
        {
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secretKey);
            byte[] messageBytes = encoding.GetBytes(requestHashString);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

    }

}
