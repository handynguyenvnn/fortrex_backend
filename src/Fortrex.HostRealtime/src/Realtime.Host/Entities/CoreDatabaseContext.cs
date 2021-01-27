using System;
using LibDatabaseEntitys;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Realtime.Host.Entities
{
    public partial class CoreDatabaseContext : DbContext
    {
        //public HighchartProjectContext()
        //{
        //}

        public CoreDatabaseContext(DbContextOptions<CoreDatabaseContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Bncthistory> Bncthistory { get; set; }
        public virtual DbSet<BonusCashBack> BonusCashBack { get; set; }
        public virtual DbSet<BonusSale> BonusSale { get; set; }
        public virtual DbSet<BuyCoin> BuyCoin { get; set; }
        public virtual DbSet<BuyCoinStatus> BuyCoinStatus { get; set; }
        public virtual DbSet<CoinList> CoinList { get; set; }
        public virtual DbSet<CoinTransaction> CoinTransaction { get; set; }
        public virtual DbSet<CoinTransactionClone> CoinTransactionClone { get; set; }
        public virtual DbSet<ContentStatic> ContentStatic { get; set; }
        public virtual DbSet<Dblog> Dblog { get; set; }
        public virtual DbSet<DepositHistory> DepositHistory { get; set; }
        public virtual DbSet<DepositProgress> DepositProgress { get; set; }
        public virtual DbSet<HistoryTransaction> HistoryTransaction { get; set; }
        public virtual DbSet<HistorysOrdersEntity> HistorysOrdersEntity { get; set; }
        public virtual DbSet<LogDervice> LogDervice { get; set; }
        public virtual DbSet<LogErc20HashLog> LogErc20HashLog { get; set; }
        public virtual DbSet<LogEthHashLog> LogEthHashLog { get; set; }
        public virtual DbSet<LogStatus> LogStatus { get; set; }
        public virtual DbSet<LoginSession> LoginSession { get; set; }
        public virtual DbSet<MailAccount> MailAccount { get; set; }
        public virtual DbSet<MailTemplate> MailTemplate { get; set; }
        public virtual DbSet<MailUserMining> MailUserMining { get; set; }
        public virtual DbSet<MarketingEmail> MarketingEmail { get; set; }
        public virtual DbSet<MarketingEmailType> MarketingEmailType { get; set; }
        public virtual DbSet<Muser> Muser { get; set; }
        public virtual DbSet<OtOtherList> OtOtherList { get; set; }
        public virtual DbSet<PackageBonusOnDay> PackageBonusOnDay { get; set; }
        public virtual DbSet<Packages> Packages { get; set; }
        public virtual DbSet<PackagesBonusF> PackagesBonusF { get; set; }
        public virtual DbSet<PackagesBonusTransaction> PackagesBonusTransaction { get; set; }
        public virtual DbSet<PackegesBonus> PackegesBonus { get; set; }
        public virtual DbSet<QaNote> QaNote { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<ScheduleTask> ScheduleTask { get; set; }
        public virtual DbSet<SellStock> SellStock { get; set; }
        public virtual DbSet<SessionLogin> SessionLogin { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<SysnDataTab> SysnDataTab { get; set; }
        public virtual DbSet<TTreeData> TTreeData { get; set; }
        public virtual DbSet<TblTempPayProfitDaily> TblTempPayProfitDaily { get; set; }
        public virtual DbSet<TblTempPayProfitDailyLog> TblTempPayProfitDailyLog { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TradeHistoryTransaction> TradeHistoryTransaction { get; set; }
        public virtual DbSet<TradePair> TradePair { get; set; }
        public virtual DbSet<TransactionCoinMethodPayment> TransactionCoinMethodPayment { get; set; }
        public virtual DbSet<TransactionSession> TransactionSession { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatus { get; set; }
        public virtual DbSet<TransactionType> TransactionType { get; set; }
        public virtual DbSet<UserBlock> UserBlock { get; set; }
        public virtual DbSet<UserBranchBalance> UserBranchBalance { get; set; }
        public virtual DbSet<UserExtension> UserExtension { get; set; }
        public virtual DbSet<UserIgnore> UserIgnore { get; set; }
        public virtual DbSet<UserLockPending> UserLockPending { get; set; }
        public virtual DbSet<UserMaxout> UserMaxout { get; set; }
        public virtual DbSet<UserPairNameMapping> UserPairNameMapping { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<UserTransfer> UserTransfer { get; set; }
        public virtual DbSet<UserVol> UserVol { get; set; }
        public virtual DbSet<UserWallet> UserWallet { get; set; }
        public virtual DbSet<UserWalletAddress> UserWalletAddress { get; set; }
        public virtual DbSet<UserWalletAmount> UserWalletAmount { get; set; }
        public virtual DbSet<UserWalletTron> UserWalletTron { get; set; }
        public virtual DbSet<UsersMarketingBonus> UsersMarketingBonus { get; set; }
        public virtual DbSet<UsersTem> UsersTem { get; set; }
        public virtual DbSet<WalletBnb> WalletBnb { get; set; }
        public virtual DbSet<WalletBnct> WalletBnct { get; set; }
        public virtual DbSet<WalletBtc> WalletBtc { get; set; }
        public virtual DbSet<Wallet_GES> Wallet_GES { get; set; }
        public virtual DbSet<Wallet_ELD> Wallet_ELD { get; set; }
        public virtual DbSet<Wallet_BRI> Wallet_BRI { get; set; }
        public virtual DbSet<WalletErc20ContractAddressMapping> WalletErc20ContractAddressMapping { get; set; }
        public virtual DbSet<WalletEthGeneral> WalletEthGeneral { get; set; }
        public virtual DbSet<WalletTrx> WalletTrx { get; set; }
        public virtual DbSet<WalletUsdt> WalletUsdt { get; set; }
        public virtual DbSet<Withdraw> Withdraw { get; set; }
        public virtual DbSet<WithdrawHistorys> WithdrawHistorys { get; set; }
        public virtual DbSet<WithdrawProcessing> WithdrawProcessing { get; set; }
        public virtual DbSet<WithdrawProgress> WithdrawProgress { get; set; }
        public virtual DbSet<WithdrawStatus> WithdrawStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Data Source=148.72.209.15;Initial Catalog=HighchartProject;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=q!#%ADEcb21;MultipleActiveResultSets=True;Max Pool Size=500");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bncthistory>(entity =>
            {
                entity.ToTable("BNCTHistory");

                entity.Property(e => e.Coin).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Uid).HasColumnName("UId");
            });

            modelBuilder.Entity<BonusCashBack>(entity =>
            {
                entity.ToTable("Bonus_Cash_Back");

                entity.HasIndex(e => new { e.UserId, e.Type, e.Bonus })
                    .HasName("Bonus_Cash_Back_Uid_Type_Bonus")
                    .IsUnique();

                entity.Property(e => e.Bonus).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<BonusSale>(entity =>
            {
                entity.Property(e => e.Bonus).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<BuyCoin>(entity =>
            {
                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.NumberCoin).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.OriginUsd)
                    .HasColumnName("OriginUSD")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.PriceUsd)
                    .HasColumnName("PriceUSD")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Transaction).HasMaxLength(128);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BuyCoinStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<CoinList>(entity =>
            {
                entity.Property(e => e.CoinContract).HasMaxLength(128);

                entity.Property(e => e.CoinName).HasMaxLength(50);

                entity.Property(e => e.CoinSymbol).HasMaxLength(10);

                entity.Property(e => e.Decimals).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TypeCoin).HasMaxLength(20);
            });

            modelBuilder.Entity<CoinTransaction>(entity =>
            {
                entity.Property(e => e.AddressWallet)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.HashCode)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PriceCoin).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.PriceUsd)
                    .HasColumnName("PriceUSD")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ServerTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CoinTransactionClone>(entity =>
            {
                entity.ToTable("CoinTransaction_Clone");

                entity.Property(e => e.AddressWallet).HasMaxLength(128);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.HashCode)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PriceCoin).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.PriceUsd)
                    .HasColumnName("PriceUSD")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ContentStatic>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.HideDate).HasColumnType("datetime");

                entity.Property(e => e.Meg)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ShowDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Dblog>(entity =>
            {
                entity.ToTable("DBLog");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<DepositHistory>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.UserId })
                    .HasName("IX_DepositHistory_userid")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AmountUsd)
                    .HasColumnName("AmountUSD")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.BlockNumber).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CoinValue).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.FromAddress).HasMaxLength(128);

                entity.Property(e => e.TxHash).HasMaxLength(128);

                entity.Property(e => e.WalletAddress).HasMaxLength(128);

                entity.Property(e => e.WalletType).HasMaxLength(20);
            });

            modelBuilder.Entity<DepositProgress>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.UserId })
                    .HasName("IX_DepositProgress_userid")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AmountUsd)
                    .HasColumnName("AmountUSD")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CoinValue).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.FromAddress).HasMaxLength(128);

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.WalletAddress).HasMaxLength(128);

                entity.Property(e => e.WalletType).HasMaxLength(20);
            });

            modelBuilder.Entity<HistoryTransaction>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.UserId, e.Type })
                    .HasName("idx_history_userid_type");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CoinBaseTransactionId).HasMaxLength(128);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.DailyRoi).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<HistorysOrdersEntity>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Fee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Filled).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FilledPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Pair).HasMaxLength(10);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<LogDervice>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.CreateOn).HasMaxLength(20);

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.UserAgent).HasMaxLength(250);
            });

            modelBuilder.Entity<LogErc20HashLog>(entity =>
            {
                entity.ToTable("Log_Erc20HashLog");

                entity.HasIndex(e => new { e.Id, e.TxHash })
                    .HasName("IX_Log_Erc20HashLog")
                    .IsUnique();

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.TxHash)
                    .IsRequired()
                    .HasColumnName("txHash")
                    .HasMaxLength(250);

                entity.Property(e => e.UserId).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<LogEthHashLog>(entity =>
            {
                entity.ToTable("Log_EthHashLog");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.TxHash)
                    .HasColumnName("txHash")
                    .HasMaxLength(250);

                entity.Property(e => e.UserId).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<LogStatus>(entity =>
            {
                entity.ToTable("Log_Status");

                entity.Property(e => e.BeforeTrx)
                    .HasColumnName("BeforeTRX")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.NowTrx)
                    .HasColumnName("NowTRX")
                    .HasColumnType("decimal(18, 8)");
            });

            modelBuilder.Entity<LoginSession>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MailAccount>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DisplayName).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Host).HasMaxLength(128);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<MailTemplate>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MailUserMining>(entity =>
            {
                entity.ToTable("Mail_UserMining");

                entity.Property(e => e.Bonus).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.NextTimeOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MarketingEmail>(entity =>
            {
                entity.Property(e => e.Body).HasColumnType("ntext");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MarketingEmailType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<Muser>(entity =>
            {
                entity.ToTable("MUser");

                entity.HasIndex(e => e.Username);

                entity.Property(e => e.CityId).HasMaxLength(50);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CountryId).HasMaxLength(50);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fa2code)
                    .HasColumnName("FA2Code")
                    .HasMaxLength(128);

                entity.Property(e => e.Fa3code)
                    .HasColumnName("FA3Code")
                    .HasMaxLength(150);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.LastActiveDate).HasColumnType("datetime");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LastIpAddress).HasMaxLength(128);

                entity.Property(e => e.LastLockDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.Node).HasMaxLength(20);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PasswordSaft).HasMaxLength(10);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WalletBch)
                    .HasColumnName("WalletBCH")
                    .HasMaxLength(128);

                entity.Property(e => e.WalletBnct)
                    .HasColumnName("WalletBNCT")
                    .HasMaxLength(128);

                entity.Property(e => e.WalletCoin).HasMaxLength(128);

                entity.Property(e => e.WalletEth)
                    .HasColumnName("WalletETH")
                    .HasMaxLength(128);

                entity.Property(e => e.WalletXrp)
                    .HasColumnName("WalletXRP")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<OtOtherList>(entity =>
            {
                entity.ToTable("OT_Other_List");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeValue)
                    .HasColumnName("Code_value")
                    .HasMaxLength(10);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasMaxLength(250);

                entity.Property(e => e.NameVn)
                    .HasColumnName("Name_vn")
                    .HasMaxLength(250);

                entity.Property(e => e.TypeCode)
                    .HasColumnName("Type_Code")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PackageBonusOnDay>(entity =>
            {
                entity.HasKey(e => e.CreateOn);

                entity.ToTable("Package_BonusOnDay");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.BonusPercent).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Packages>(entity =>
            {
                entity.Property(e => e.FinishDay).HasColumnType("decimal(5, 1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.PercentOnDay).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.PercentTotal).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.PlusPercent).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.PriceFrom).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.PriceTo).HasColumnType("decimal(18, 8)");
            });

            modelBuilder.Entity<PackagesBonusF>(entity =>
            {
                entity.ToTable("Packages_Bonus_F");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Percent).HasColumnType("decimal(18, 5)");
            });

            modelBuilder.Entity<PackagesBonusTransaction>(entity =>
            {
                entity.ToTable("Packages_Bonus_Transaction");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Bonus).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Day).HasColumnType("datetime");

                entity.Property(e => e.PercentAmount).HasColumnType("decimal(18, 8)");
            });

            modelBuilder.Entity<PackegesBonus>(entity =>
            {
                entity.ToTable("Packeges_Bonus");

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.IsActive, e.IsProfit, e.CreateOn });

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Invested).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SharePercent).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.SharePrice).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ShareTotal).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.StartProfitDate).HasColumnType("datetime");

                entity.Property(e => e.TempProfit).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.TempStock).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<QaNote>(entity =>
            {
                entity.ToTable("QA_Note");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ScheduleTask>(entity =>
            {
                entity.Property(e => e.LastEndUtc).HasColumnType("datetime");

                entity.Property(e => e.LastStartUtc).HasColumnType("datetime");

                entity.Property(e => e.LastSuccessUtc).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(350);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(350);
            });

            modelBuilder.Entity<SellStock>(entity =>
            {
                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ResponseAmount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ResposeFee).HasColumnType("decimal(18, 8)");
            });

            modelBuilder.Entity<SessionLogin>(entity =>
            {
                entity.Property(e => e.Token).HasMaxLength(200);
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value).HasMaxLength(250);
            });

            modelBuilder.Entity<SysnDataTab>(entity =>
            {
                entity.ToTable("Sysn_Data_Tab");

                entity.Property(e => e.CreateOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExtraData).HasMaxLength(4000);
            });

            modelBuilder.Entity<TTreeData>(entity =>
            {
                entity.ToTable("T_TreeData");

                entity.HasIndex(e => e.ParentId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblTempPayProfitDaily>(entity =>
            {
                entity.ToTable("tbl_tempPayProfitDaily");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.AmountBeforeaDay).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.AmountUsd)
                    .HasColumnName("AmountUSD")
                    .HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ApprovedbyAdmin).HasDefaultValueSql("((0))");

                entity.Property(e => e.ByUser).HasMaxLength(50);

                entity.Property(e => e.CreatePay).HasColumnType("datetime");

                entity.Property(e => e.Descriptions).HasMaxLength(500);

                entity.Property(e => e.TotalInvest).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Txhash)
                    .HasColumnName("txhash")
                    .HasMaxLength(256);

                entity.Property(e => e.WalletEth)
                    .IsRequired()
                    .HasColumnName("WalletETH")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<TblTempPayProfitDailyLog>(entity =>
            {
                entity.ToTable("tbl_tempPayProfitDaily_log");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.AmountBeforeaDay).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CreatePay).HasColumnType("datetime");

                entity.Property(e => e.TotalInvest).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Userid).HasMaxLength(50);

                entity.Property(e => e.WalletEth)
                    .HasColumnName("WalletETH")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(250);

                entity.Property(e => e.ModifyData).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ReplyBy).HasMaxLength(50);

                entity.Property(e => e.Subject).HasMaxLength(150);
            });

            modelBuilder.Entity<TradeHistoryTransaction>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BuyExchange).HasMaxLength(50);

                entity.Property(e => e.BuyPrice).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CoinName).HasMaxLength(30);

                entity.Property(e => e.CoinPair).HasMaxLength(50);

                entity.Property(e => e.PercentDifference).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SellExchange).HasMaxLength(50);

                entity.Property(e => e.SellPrice).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.TradeAt).HasColumnType("datetime");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("TransactionID")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<TradePair>(entity =>
            {
                entity.Property(e => e.FeeTrade).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Fsym).HasMaxLength(10);

                entity.Property(e => e.PairName).HasMaxLength(20);

                entity.Property(e => e.Tsym).HasMaxLength(10);
            });

            modelBuilder.Entity<TransactionCoinMethodPayment>(entity =>
            {
                entity.ToTable("TransactionCoin_MethodPayment");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descriptions).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Symbols).HasMaxLength(50);
            });

            modelBuilder.Entity<TransactionSession>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TypeTransaction).HasMaxLength(20);
            });

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<UserBlock>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("User_Block");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<UserBranchBalance>(entity =>
            {
                entity.ToTable("User_Branch_Balance");

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.Status, e.CreateDate });

                entity.Property(e => e.Bonus).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.BranchLeft).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.BranchRight).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LeftAmount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.LeftReset).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MaxInvest).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.RightAmount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.RightReset).HasColumnType("decimal(18, 8)");
            });

            modelBuilder.Entity<UserExtension>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User_Extention");

                entity.ToTable("User_Extension");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.BackSideUrl)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Country)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Firstname).HasMaxLength(128);

                entity.Property(e => e.FontSideUrl)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.IdentificationNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdentificationType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname).HasMaxLength(128);

                entity.Property(e => e.PhoneNumber).HasMaxLength(30);

                entity.Property(e => e.SelfieUrl)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<UserIgnore>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("User_Ignore");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.IgnoreDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserLockPending>(entity =>
            {
                entity.ToTable("User_LockPending");

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserMaxout>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("User_Maxout");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.MaxOutDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserPairNameMapping>(entity =>
            {
                entity.ToTable("User_PairName_Mapping");

                entity.Property(e => e.PairName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.ToTable("User_Role_Mapping");
            });

            modelBuilder.Entity<UserTransfer>(entity =>
            {
                entity.ToTable("User_Transfer");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ApplyOn).HasColumnType("datetime");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.ResAmount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ResFee).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Type).HasMaxLength(5);
            });

            modelBuilder.Entity<UserVol>(entity =>
            {
                entity.ToTable("User_Vol");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.TotalTrade).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserWallet>(entity =>
            {
                entity.ToTable("User_Wallet");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.LastAmount).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.WalletAddress).HasMaxLength(128);
            });

            modelBuilder.Entity<UserWalletAddress>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("User_WalletAddress");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.BonusBranch).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.BonusCommission).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.BonusLucky).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.BonusSale).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MaxInvest).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MoneyBTC)
                    .HasColumnName("MoneyBTC")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MoneyDemo)
                    .HasColumnType("decimal(18, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MoneyETH)
                    .HasColumnName("MoneyETH")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MoneyUSD)
                    .HasColumnName("MoneyUSD")
                    .HasColumnType("decimal(18, 8)");
                entity.Property(e => e.MoneyGES)
                  .HasColumnName("MoneyGES")
                  .HasColumnType("decimal(18, 8)");
                entity.Property(e => e.MoneyELD)
                  .HasColumnName("MoneyELD")
                  .HasColumnType("decimal(18, 8)");
                entity.Property(e => e.MoneyBRI)
                  .HasColumnName("MoneyBRI")
                  .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.TotalBonus).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.WalletBTC)
                    .HasColumnName("WalletBTC")
                    .HasMaxLength(128);

                entity.Property(e => e.WalletETH)
                    .HasColumnName("WalletETH")
                    .HasMaxLength(128);

                entity.Property(e => e.WalletMy).HasMaxLength(128);

                entity.Property(e => e.WalletStocks).HasMaxLength(128);
            });

            modelBuilder.Entity<UserWalletAmount>(entity =>
            {
                entity.ToTable("User_Wallet_Amount");

                entity.Property(e => e.Amount40).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Amount60).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.AmountXrp)
                    .HasColumnName("AmountXRP")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.IsTransferXrp).HasColumnName("IsTransferXRP");
            });

            modelBuilder.Entity<UserWalletTron>(entity =>
            {
                entity.ToTable("User_WalletTron");

                entity.Property(e => e.Bonus20Percent).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Bonus73Percent).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Bonus7Percent).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<UsersMarketingBonus>(entity =>
            {
                entity.ToTable("Users_Marketing_Bonus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UsersTem>(entity =>
            {
                entity.ToTable("Users_Tem");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<WalletBnb>(entity =>
            {
                entity.ToTable("Wallet_BNB");

                entity.Property(e => e.CoinAddress).HasMaxLength(128);

                entity.Property(e => e.CoinContract).HasMaxLength(128);

                entity.Property(e => e.CoinName).HasMaxLength(50);

                entity.Property(e => e.CoinPrivateKey).HasMaxLength(255);

                entity.Property(e => e.CoinPublicKey).HasMaxLength(255);

                entity.Property(e => e.CoinSymbol).HasMaxLength(10);
            });

            modelBuilder.Entity<WalletBnct>(entity =>
            {
                entity.ToTable("Wallet_BNCT");

                entity.Property(e => e.CoinAddress).HasMaxLength(128);

                entity.Property(e => e.CoinContract).HasMaxLength(128);

                entity.Property(e => e.CoinName).HasMaxLength(50);

                entity.Property(e => e.CoinPrivateKey).HasMaxLength(255);

                entity.Property(e => e.CoinPublicKey).HasMaxLength(255);

                entity.Property(e => e.CoinSymbol).HasMaxLength(10);
            });

            modelBuilder.Entity<WalletBtc>(entity =>
            {
                entity.ToTable("Wallet_BTC");

                entity.Property(e => e.CoinAddress).HasMaxLength(128);

                entity.Property(e => e.CoinContract).HasMaxLength(128);

                entity.Property(e => e.CoinName).HasMaxLength(50);

                entity.Property(e => e.CoinPrivateKey).HasMaxLength(255);

                entity.Property(e => e.CoinPublicKey).HasMaxLength(255);

                entity.Property(e => e.CoinSymbol).HasMaxLength(10);
            });

            modelBuilder.Entity<WalletErc20ContractAddressMapping>(entity =>
            {
                entity.ToTable("WalletErc20ContractAddress_Mapping");

                entity.Property(e => e.CoinAddress).HasMaxLength(128);

                entity.Property(e => e.CoinContract).HasMaxLength(128);

                entity.Property(e => e.CoinName).HasMaxLength(50);

                entity.Property(e => e.CoinSymbol).HasMaxLength(10);

                entity.Property(e => e.Type).HasMaxLength(25);
            });

            modelBuilder.Entity<WalletEthGeneral>(entity =>
            {
                entity.ToTable("Wallet_ETH_General");

                entity.Property(e => e.CoinAddress).HasMaxLength(128);

                entity.Property(e => e.CoinContract).HasMaxLength(128);

                entity.Property(e => e.CoinName).HasMaxLength(50);

                entity.Property(e => e.CoinPrivateKey).HasMaxLength(255);

                entity.Property(e => e.CoinPublicKey).HasMaxLength(255);

                entity.Property(e => e.CoinSymbol).HasMaxLength(10);
            });

            modelBuilder.Entity<WalletTrx>(entity =>
            {
                entity.ToTable("Wallet_TRX");

                entity.Property(e => e.CoinAddress).HasMaxLength(128);

                entity.Property(e => e.CoinContract).HasMaxLength(128);

                entity.Property(e => e.CoinName).HasMaxLength(50);

                entity.Property(e => e.CoinPrivateKey).HasMaxLength(255);

                entity.Property(e => e.CoinPublicKey).HasMaxLength(255);

                entity.Property(e => e.CoinSymbol).HasMaxLength(10);
            });

            modelBuilder.Entity<WalletUsdt>(entity =>
            {
                entity.ToTable("Wallet_USDT");

                entity.Property(e => e.CoinAddress).HasMaxLength(128);

                entity.Property(e => e.CoinContract).HasMaxLength(128);

                entity.Property(e => e.CoinName).HasMaxLength(50);

                entity.Property(e => e.CoinPrivateKey).HasMaxLength(255);

                entity.Property(e => e.CoinPublicKey).HasMaxLength(255);

                entity.Property(e => e.CoinSymbol).HasMaxLength(10);
            });

            modelBuilder.Entity<Withdraw>(entity =>
            {
                entity.Property(e => e.AmountGet).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.AmountSet).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ConfirmDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Fee).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.HashCode).HasMaxLength(128);

                entity.Property(e => e.IsConfirmEmail).HasDefaultValueSql("((0))");

                entity.Property(e => e.TokenConfirm).HasMaxLength(128);

                entity.Property(e => e.Transaction).HasMaxLength(128);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WithdrawHistorys>(entity =>
            {
                entity.Property(e => e.AmountGet).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AmountSet).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Fee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TxHash).HasMaxLength(128);

                entity.Property(e => e.WalletAddress).HasMaxLength(128);

                entity.Property(e => e.WalletType).HasMaxLength(20);
            });

            modelBuilder.Entity<WithdrawProcessing>(entity =>
            {
                entity.Property(e => e.AmountGet).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.AmountSet).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Fee).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.HashCode).HasMaxLength(128);

                entity.Property(e => e.Transaction).HasMaxLength(128);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WithdrawProgress>(entity =>
            {
                entity.Property(e => e.AmountGet).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AmountSet).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Fee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TxHash).HasMaxLength(128);

                entity.Property(e => e.WalletAddress).HasMaxLength(128);

                entity.Property(e => e.WalletType).HasMaxLength(20);
            });

            modelBuilder.Entity<WithdrawStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
