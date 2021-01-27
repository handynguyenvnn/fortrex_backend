using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Coins
{
    public class ERC20TokenInfo
    {
        public string address { get; set; }
        public string name { get; set; }
        public decimal decimals { get; set; }
        public string symbol { get; set; }
        public decimal totalSupply { get; set; }
        public string owner { get; set; }
        public int txsCount { get; set; }
        public int transfersCount { get; set; }
        public decimal lastUpdated { get; set; }
        public int issuancesCount { get; set; }
        public int holdersCount { get; set; }
        //public bool price { get; set; }
    }
    public class ERC20GetAddressHistoryResponse
    {
        public decimal blockNumber { get; set; }
        public decimal timestamp { get; set; }
        public string transactionHash { get; set; }
        public ERC20TokenInfo tokenInfo { get; set; }
        public string type { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public decimal value { get; set; }
    }
    public class ERC20GetAddressHistoryListResponse
    {
        public List<ERC20GetAddressHistoryResponse> operations { get; set; }
    }

    // get transaction info
    public class Erc20Gettransactioninfo
    {
        public string hash { get; set; }
        public decimal timeStamp { get; set; }
        public decimal blockNumber { get; set; }
        public int confirmations { get; set; }
        public bool success { get; set; }
        public string blockHash { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public decimal value { get; set; }
        public string input { get; set; }
        public decimal gasUsed { get; set; }
        public decimal gasLimit { get; set; }
        public List<Erc20GettransactioninfoOperations> operations { get; set; }

    }
    public class Erc20GettransactioninfoOperations
    {
        public string transactionHash { get; set; }
        public decimal timeStamp { get; set; }
        public decimal value { get; set; }
        public decimal intValue { get; set; }
        public string type { get; set; }
        public bool isEth { get; set; }
        public string from { get; set; }
        public string to { get; set; }

    }

    #region transaction ethereum response
    public class EthTransactionByAddress
    {
        public decimal blockNumber { get; set; }
        public decimal timeStamp { get; set; }
        public string hash { get; set; }
        public int nonce { get; set; }
        public string blockHash { get; set; }
        public string from { get; set; }
        public string contractAddress { get; set; }
        public string to { get; set; }
        public decimal value { get; set; }
        public int transactionIndex { get; set; }
        public int gas { get; set; }
        public decimal gasPrice { get; set; }
        public decimal gasUsed { get; set; }
        public decimal cumulativeGasUsed { get; set; }
        public string input { get; set; }
        public int confirmations { get; set; }
    }
    public class EthTransactionByAddressResponse
    {
        public string message { get; set; }
        public int status { get; set; }
        public List<EthTransactionByAddress> result { get; set; }
    }

    public class blockcypher_EthTransactionByAddress
    {
        public string hash { get; set; }
        public List<string> addresses { get; set; }
        public int confirmations { get; set; }

    }
    public class blockcypher_addresses
    {

        public string addresses { get; set; }
    }
    #endregion
}
