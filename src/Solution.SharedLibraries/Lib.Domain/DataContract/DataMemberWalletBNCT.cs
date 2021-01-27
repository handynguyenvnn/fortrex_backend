using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.DataContract
{
    [DataContract]
    public class DataMemberWalletETH
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinContract { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }

    }
    [DataContract]
    public class DataMemberWalletBNCT
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinContract { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }
       
    }
    [DataContract]
    public class DataMemberWalletUSDT
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }

    }
    [DataContract]
    public class DataMemberWalletBTC
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }

    }
    [DataContract]
    public class DataMemberWalletBNB
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }
        [DataMember]
        public string CoinContract { get; set; }
    }
    [DataContract]
    public class DataMemberWalletGES
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }
        [DataMember]
        public string CoinContract { get; set; }
    }
    [DataContract]
    public class DataMemberWalletELD
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }
        [DataMember]
        public string CoinContract { get; set; }
    }
    [DataContract]
    public class DataMemberWalletBRI
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }
        [DataMember]
        public string CoinContract { get; set; }
    }
    [DataContract]
    public class DataMemberWalletXRP
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }
        [DataMember]
        public string Tag { get; set; }
    }
    [DataContract]
    public class DataMemberWalletEOS
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }
        [DataMember]
        public string MEMO { get; set; }
    }
    [DataContract]
    public class DataMemberWalletTRX
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string CoinName { get; set; }
        [DataMember]
        public string CoinSymbol { get; set; }
        [DataMember]
        public string CoinAddress { get; set; }
    }
}
