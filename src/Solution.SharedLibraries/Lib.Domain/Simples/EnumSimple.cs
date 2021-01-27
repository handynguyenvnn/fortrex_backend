using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Simples
{
    public enum MethodPayment
    {
        BTC = 1,
        ETH = 2,
        USD = 3,
        GES = 4,
        ELD = 5,
        BRI = 6
    }

    public enum InvestByType
    {
        USD = 1,
        GES = 2,
        DEMO = 3,
        ELD = 4,
        BRI = 5
    }

    public enum BuycoinStatus
    {
        Pending = 1,
        Approve = 2,
        Removed = 3,
        Canceled = 4
    }

    public enum HistoryTransactionType
    {
        Invest = 1,
        Bonus = 2,
        Deposit = 3,
        SystemBonus = 6,
        Withdraw = 7,
        Profit = 8,
        BonusCommission = 11,
        CommissionLevel = 12,
        BonusVOL = 13,
        BonusCashBack = 14,
        BonusExchanges = 15,
        MasterIBBonus = 16,
        VolumnLevelTrade = 17,
        BuyMasterIB = 18,
        BonusVolunmTrade = 19
    }

    public enum HistoryTransactionStatus
    {
        Default = 0,
        F1 = 1,
        F2 = 2,
        F3 = 3,
        F4 = 4,
        F5 = 5
    }

    public enum WithdrawType
    {
        BTC = 1,
        ETH = 2,
        USD = 3
    }
    public enum PromoStatus
    {
        NotSet = 0,
        BonusCode = 1
    }
    public enum WithdrawStatus
    {
        Pending = 1,
        Approve = 2,
        Removed = 3,
        Canceled = 4,
        Completed = 5,
        UnconfirmedEmail = 6
    }
    public enum MarketingEmailType
    {
        SendAll = 1,
        Send_To_User_Received = 2,
        SendTo_ALL_Users_In_List_Table_Temp=3,
        Send_To_Lock_Account = 4
    }

    public enum WithdrawBy
    {
        Normal = 1,
        Bonus = 2,
        Reinvestment = 3
    }

    public enum LogType
    {
        Default = -1,
        Normal = 0,
        Bonus = 1,
        BonusBranch = 2,
        LuckyBonus = 3,
        ToolBranch = 4,
        BTCNotAddress = 5,
        BTCDefault = 6,
        ReturnBonus = 7,
		ETHNotAddress = 8,
        ETHDefault = 9,
		RetryDeposit = 10
    }

    public enum BranchStatus
    {
        Avalible = 1,
        Processing = 2,
        Completed = 3
    }
    public enum TradingStatus
    {
        Win = 1,
        Lose = -1,
        Pending = 0,
        Backrefund = 2,
    }
    public enum TransactionHistoryStatus
    {
        Trading = 1,
        Systembonus = 6,
        Directbonus = 11,
        TransferUSD = 12,
    }
    public enum CandlestickCloseType
    {
        LOSE = 1,
        WIN = 2,
        PENDING = 0
    }
}
