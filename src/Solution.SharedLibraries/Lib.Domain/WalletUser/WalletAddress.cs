using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lib.Domain.Marketings
{
    public class WalletAddressTemplate
    {
       
        [ScaffoldColumn(false)]
        [Display(Name = "Id")]
        public int id { get; set; }
        // [ScaffoldColumn(false)]
        [UIHint("BizSmallText")]
        [Display(Name = "Userid")]
        public int Userid { get; set; }
        [UIHint("BizText")]
        //[ScaffoldColumn(false)]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [UIHint("BizSmallText")]
        [Display(Name = "MoneyUSD")]
        public decimal MoneyUSD { get; set; }
        // [ScaffoldColumn(false)]
        [UIHint("BizSmallText")]
        [Display(Name = "BonusLucky")]
        public decimal BonusLucky { get; set; }
       // [ScaffoldColumn(false)]
        [Display(Name = "BonusCommission")]
        [UIHint("BizSmallText")]
        public decimal BonusCommission { get; set; }
        // [ScaffoldColumn(false)]
        [UIHint("BizSmallText")]
        [Display(Name = "MaxInvest")]
        public decimal MaxInvest { get; set; }
        // [ScaffoldColumn(false)]
        [UIHint("BizSmallText")]
        [Display(Name = "WalletStocks")]
        public string WalletStocks { get; set; }
        // [ScaffoldColumn(false)]
        [UIHint("BizSmallText")]
        [Display(Name = "TotalBonus")]
        public decimal TotalBonus { get; set; }
        // [ScaffoldColumn(false)]
        [UIHint("BizSmallText")]
        [Display(Name = "BonusSale")]
        public decimal BonusSale { get; set; }
        // [ScaffoldColumn(false)]
        [UIHint("BizSmallText")]
        [Display(Name = "MoneyDemo")]
        public decimal MoneyDemo { get; set; }
        // [ScaffoldColumn(false)]
        [UIHint("BizSmallText")]
        [Display(Name = "MasterIB")]
        public decimal MasterIB { get; set; }
        // [ScaffoldColumn(false)]
        [UIHint("BizSmallText")]
        [Display(Name = "LevelId")]
        public int LevelId { get; set; }

    }
}
