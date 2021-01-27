using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Coins
{
    public class ERC20TokenTransferByAddress
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
        public string tokenName { get; set; }
        public string tokenSymbol { get; set; }
        public int tokenDecimal { get; set; }
        public int transactionIndex { get; set; }
        public int gas { get; set; }
        public decimal gasPrice { get; set; }
        public decimal gasUsed { get; set; }
        public decimal cumulativeGasUsed { get; set; }
        public string input { get; set; }
        public int confirmations { get; set; }
    }
    public class ERC20TokenTransferByAddressResponse
    {
        public string message { get; set; }
        public int status { get; set; }
        public List<ERC20TokenTransferByAddress> result { get; set; }
    }


    // get transaction info
    public class Erc20Gettransactioninfo
    {
        public decimal blockNumber { get; set; }
        public decimal timeStamp { get; set; }
        public string hash { get; set; }
        public string blockHash { get; set; }
        public string from { get; set; }
        public string contractAddress { get; set; }
        public string to { get; set; }
        public decimal value { get; set; }
        public decimal gasUsed { get; set; }
        public decimal gasLimit { get; set; }
        public string input { get; set; }
        public int confirmations { get; set; }
        public bool success { get; set; }
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
