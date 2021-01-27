namespace LibDatabaseEntitys
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CoreExchangeDB : DbContext
    {
        public CoreExchangeDB()
            : base("name=CoreExchangeDB")
        {
        }

        public virtual DbSet<BNCTHistory> BNCTHistories { get; set; }
        public virtual DbSet<Bonus_Cash_Back> Bonus_Cash_Back { get; set; }
        public virtual DbSet<BonusSale> BonusSales { get; set; }
        public virtual DbSet<BuyCoin> BuyCoins { get; set; }
        public virtual DbSet<BuyCoinStatu> BuyCoinStatus { get; set; }
        public virtual DbSet<CoinList> CoinLists { get; set; }
        public virtual DbSet<CoinTransaction> CoinTransactions { get; set; }
        public virtual DbSet<CoinTransaction_Clone> CoinTransaction_Clone { get; set; }
        public virtual DbSet<ContentStatic> ContentStatics { get; set; }
        public virtual DbSet<DBLog> DBLogs { get; set; }
        public virtual DbSet<DepositHistory> DepositHistories { get; set; }
        public virtual DbSet<DepositProgress> DepositProgresses { get; set; }
        public virtual DbSet<HistorysOrdersEntity> HistorysOrdersEntities { get; set; }
        public virtual DbSet<HistoryTransactionEntity> HistoryTransactionEntitys { get; set; }
        public virtual DbSet<Log_Erc20HashLog> Log_Erc20HashLog { get; set; }
        public virtual DbSet<Log_EthHashLog> Log_EthHashLog { get; set; }
        public virtual DbSet<Log_Status> Log_Status { get; set; }
        public virtual DbSet<LogDervice> LogDervices { get; set; }
        public virtual DbSet<LoginSession> LoginSessions { get; set; }
        public virtual DbSet<Mail_UserMining> Mail_UserMining { get; set; }
        public virtual DbSet<MailAccount> MailAccounts { get; set; }
        public virtual DbSet<MailTemplateEntity> MailTemplates { get; set; }
        public virtual DbSet<MarketingEmailEntity> MarketingEmails { get; set; }
        public virtual DbSet<MarketingEmailTypeEntity> MarketingEmailTypes { get; set; }
        public virtual DbSet<MUser> MUsers { get; set; }
        public virtual DbSet<OT_Other_List> OT_Other_List { get; set; }
        public virtual DbSet<Package_BonusOnDay> Package_BonusOnDay { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Packages_Bonus_F> Packages_Bonus_F { get; set; }
        public virtual DbSet<Packages_Bonus_Transaction> Packages_Bonus_Transaction { get; set; }
        public virtual DbSet<Packeges_BonusEntitys> Packeges_BonusEntitys { get; set; }
        public virtual DbSet<QA_Note> QA_Note { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ScheduleTask> ScheduleTasks { get; set; }
        public virtual DbSet<SellStock> SellStocks { get; set; }
        public virtual DbSet<SessionLogin> SessionLogins { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Sysn_Data_Tab> Sysn_Data_Tab { get; set; }
        public virtual DbSet<T_TreeData> T_TreeData { get; set; }
        public virtual DbSet<tbl_tempPayProfitDaily> tbl_tempPayProfitDaily { get; set; }
        public virtual DbSet<tbl_tempPayProfitDaily_log> tbl_tempPayProfitDaily_log { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TradeHistoryTransactionEntitys> TradeHistoryTransactionsEntitys { get; set; }
        public virtual DbSet<TradePair> TradePairs { get; set; }
        public virtual DbSet<TransactionCoin_MethodPayment> TransactionCoin_MethodPayment { get; set; }
        public virtual DbSet<TransactionSession> TransactionSessions { get; set; }
        public virtual DbSet<TransactionStatu> TransactionStatus { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<User_Block> User_Block { get; set; }
        public virtual DbSet<User_Branch_Balance> User_Branch_Balance { get; set; }
        public virtual DbSet<User_ExtensionEntity> User_ExtensionEntities { get; set; }
        public virtual DbSet<User_Ignore> User_Ignore { get; set; }
        public virtual DbSet<User_LockPending> User_LockPending { get; set; }
        public virtual DbSet<User_Maxout> User_Maxout { get; set; }
        public virtual DbSet<User_PairName_Mapping> User_PairName_Mapping { get; set; }
        public virtual DbSet<User_Role_Mapping> User_Role_Mapping { get; set; }
        public virtual DbSet<User_Transfer> User_Transfer { get; set; }
        public virtual DbSet<User_Vol> User_Vol { get; set; }
        public virtual DbSet<User_Wallet> User_Wallet { get; set; }
        public virtual DbSet<User_Wallet_Amount> User_Wallet_Amount { get; set; }
        public virtual DbSet<User_WalletAddress> User_WalletAddress { get; set; }
        public virtual DbSet<User_WalletTron> User_WalletTron { get; set; }
        public virtual DbSet<Users_Marketing_Bonus> Users_Marketing_Bonus { get; set; }
        public virtual DbSet<Users_Tem> Users_Tem { get; set; }
        public virtual DbSet<Wallet_BNB> Wallet_BNB { get; set; }
        public virtual DbSet<Wallet_GES> Wallet_GES { get; set; }
        public virtual DbSet<Wallet_ELD> Wallet_ELD { get; set; }
        public virtual DbSet<Wallet_BRI> Wallet_BRI { get; set; }
        public virtual DbSet<Wallet_BTC> Wallet_BTC { get; set; }
        public virtual DbSet<Wallet_ETH_General> Wallet_ETH_General { get; set; }
        public virtual DbSet<Wallet_TRX> Wallet_TRX { get; set; }
        public virtual DbSet<Wallet_USDT> Wallet_USDT { get; set; }
        public virtual DbSet<WalletErc20ContractAddress_Mapping> WalletErc20ContractAddress_Mapping { get; set; }
        public virtual DbSet<WithdrawEntity> WithdrawEntitys { get; set; }
        public virtual DbSet<WithdrawHistory> WithdrawHistoryEntitys { get; set; }
        public virtual DbSet<WithdrawProcessing> WithdrawProcessings { get; set; }
        public virtual DbSet<WithdrawProgress> WithdrawProgresses { get; set; }
        public virtual DbSet<WithdrawStatu> WithdrawStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BNCTHistory>()
                .Property(e => e.Coin)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Bonus_Cash_Back>()
                .Property(e => e.Bonus)
                .HasPrecision(18, 3);

            modelBuilder.Entity<BonusSale>()
                .Property(e => e.Bonus)
                .HasPrecision(18, 8);

            modelBuilder.Entity<BuyCoin>()
                .Property(e => e.NumberCoin)
                .HasPrecision(18, 8);

            modelBuilder.Entity<BuyCoin>()
                .Property(e => e.OriginUSD)
                .HasPrecision(18, 8);

            modelBuilder.Entity<BuyCoin>()
                .Property(e => e.PriceUSD)
                .HasPrecision(18, 8);

            modelBuilder.Entity<CoinTransaction>()
                .Property(e => e.PriceCoin)
                .HasPrecision(18, 8);

            modelBuilder.Entity<CoinTransaction>()
                .Property(e => e.PriceUSD)
                .HasPrecision(18, 8);

            modelBuilder.Entity<CoinTransaction_Clone>()
                .Property(e => e.PriceCoin)
                .HasPrecision(18, 8);

            modelBuilder.Entity<CoinTransaction_Clone>()
                .Property(e => e.PriceUSD)
                .HasPrecision(18, 8);

            modelBuilder.Entity<DepositHistory>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DepositHistory>()
                .Property(e => e.CoinValue)
                .HasPrecision(18, 8);

            modelBuilder.Entity<DepositHistory>()
                .Property(e => e.AmountUSD)
                .HasPrecision(18, 8);

            modelBuilder.Entity<DepositProgress>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DepositProgress>()
                .Property(e => e.CoinValue)
                .HasPrecision(18, 8);

            modelBuilder.Entity<DepositProgress>()
                .Property(e => e.AmountUSD)
                .HasPrecision(18, 8);

            modelBuilder.Entity<HistoryTransactionEntity>()
                .Property(e => e.Amount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<HistoryTransactionEntity>()
                .Property(e => e.DailyRoi)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Log_Status>()
                .Property(e => e.BeforeTRX)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Log_Status>()
                .Property(e => e.NowTRX)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Mail_UserMining>()
                .Property(e => e.Bonus)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Package>()
                .Property(e => e.PriceFrom)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Package>()
                .Property(e => e.PriceTo)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Package>()
                .Property(e => e.PercentOnDay)
                .HasPrecision(18, 5);

            modelBuilder.Entity<Package>()
                .Property(e => e.PercentTotal)
                .HasPrecision(18, 5);

            modelBuilder.Entity<Package>()
                .Property(e => e.PlusPercent)
                .HasPrecision(18, 5);

            modelBuilder.Entity<Package>()
                .Property(e => e.FinishDay)
                .HasPrecision(5, 1);

            modelBuilder.Entity<Packages_Bonus_F>()
                .Property(e => e.Percent)
                .HasPrecision(18, 5);

            modelBuilder.Entity<Packages_Bonus_Transaction>()
                .Property(e => e.Bonus)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Packages_Bonus_Transaction>()
                .Property(e => e.PercentAmount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Packeges_BonusEntitys>()
                .Property(e => e.Invested)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Packeges_BonusEntitys>()
                .Property(e => e.SharePercent)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Packeges_BonusEntitys>()
                .Property(e => e.SharePrice)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Packeges_BonusEntitys>()
                .Property(e => e.ShareTotal)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Packeges_BonusEntitys>()
                .Property(e => e.TempStock)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Packeges_BonusEntitys>()
                .Property(e => e.TempProfit)
                .HasPrecision(18, 8);

            modelBuilder.Entity<QA_Note>()
                .Property(e => e.Amount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<SellStock>()
                .Property(e => e.RequestAmount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<SellStock>()
                .Property(e => e.ResposeFee)
                .HasPrecision(18, 8);

            modelBuilder.Entity<SellStock>()
                .Property(e => e.ResponseAmount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<tbl_tempPayProfitDaily>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_tempPayProfitDaily>()
                .Property(e => e.TotalInvest)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_tempPayProfitDaily>()
                .Property(e => e.AmountBeforeaDay)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_tempPayProfitDaily>()
                .Property(e => e.AmountUSD)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_tempPayProfitDaily_log>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_tempPayProfitDaily_log>()
                .Property(e => e.TotalInvest)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_tempPayProfitDaily_log>()
                .Property(e => e.AmountBeforeaDay)
                .HasPrecision(18, 4);

            modelBuilder.Entity<TradeHistoryTransactionEntitys>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TradeHistoryTransactionEntitys>()
                .Property(e => e.BuyPrice)
                .HasPrecision(18, 8);

            modelBuilder.Entity<TradeHistoryTransactionEntitys>()
                .Property(e => e.SellPrice)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Branch_Balance>()
                .Property(e => e.LeftAmount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Branch_Balance>()
                .Property(e => e.RightAmount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Branch_Balance>()
                .Property(e => e.LeftReset)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Branch_Balance>()
                .Property(e => e.RightReset)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Branch_Balance>()
                .Property(e => e.MaxInvest)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Branch_Balance>()
                .Property(e => e.Bonus)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Branch_Balance>()
                .Property(e => e.BranchLeft)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Branch_Balance>()
                .Property(e => e.BranchRight)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_ExtensionEntity>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<User_ExtensionEntity>()
                .Property(e => e.IdentificationType)
                .IsUnicode(false);

            modelBuilder.Entity<User_ExtensionEntity>()
                .Property(e => e.IdentificationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User_Transfer>()
                .Property(e => e.Amount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Transfer>()
                .Property(e => e.ResFee)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Transfer>()
                .Property(e => e.ResAmount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Vol>()
                .Property(e => e.TotalTrade)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Wallet>()
                .Property(e => e.Amount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Wallet>()
                .Property(e => e.LastAmount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Wallet_Amount>()
                .Property(e => e.Amount40)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Wallet_Amount>()
                .Property(e => e.Amount60)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_Wallet_Amount>()
                .Property(e => e.AmountXRP)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.MoneyBTC)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.MoneyETH)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.MoneyUSD)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.BonusBranch)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.BonusLucky)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.BonusCommission)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.MaxInvest)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.TotalBonus)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.BonusSale)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletAddress>()
                .Property(e => e.MoneyDemo)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletTron>()
                .Property(e => e.Bonus73Percent)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletTron>()
                .Property(e => e.Bonus20Percent)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User_WalletTron>()
                .Property(e => e.Bonus7Percent)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Users_Marketing_Bonus>()
                .Property(e => e.type)
                .IsFixedLength();

            modelBuilder.Entity<WithdrawEntity>()
                .Property(e => e.AmountSet)
                .HasPrecision(18, 8);

            modelBuilder.Entity<WithdrawEntity>()
                .Property(e => e.Fee)
                .HasPrecision(18, 8);

            modelBuilder.Entity<WithdrawEntity>()
                .Property(e => e.AmountGet)
                .HasPrecision(18, 8);

            modelBuilder.Entity<WithdrawProcessing>()
                .Property(e => e.AmountSet)
                .HasPrecision(18, 8);

            modelBuilder.Entity<WithdrawProcessing>()
                .Property(e => e.Fee)
                .HasPrecision(18, 8);

            modelBuilder.Entity<WithdrawProcessing>()
                .Property(e => e.AmountGet)
                .HasPrecision(18, 8);
        }
    }
}
