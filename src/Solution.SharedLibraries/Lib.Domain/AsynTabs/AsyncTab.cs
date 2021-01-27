using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Domain.Simples;

namespace Lib.Domain.AsynTabs
{
    public enum AsynTabType
    {
        PROCESS_PACKAGE = 1,// agency
        PROCESS_VOLUME_SYSTEM = 2 //trading
    }

    public enum AsynTabStatus
    {
        PENDING = 1,
        PROCESS = 2,
        COMPLETED = 3,
        FAIL = 4,
        PROCESS_LEVEL = 5,
        COMPLETED_LEVEL = 6,
        FAIL_LEVEL = 7
    }

    public enum PocessVolumnSystem
    {
        PENDING = 1,
        BONUS_MASTERIB = 2,
        BONUS_MASTERIB_COMPLETE = 3,
        FAIL = 4,
        BONUS_LEVEL_PROCESS = 5,
        BONUS_LEVEL_COMPLETE = 6,
        BONUS_LEVEL_FAIL = 7
    }

    public class AsynTab
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string ExtraData { get; set; }
        public DateTime CreateOn { get; set; }
    }

    public class BonusLevelExtraData
    {
        public BonusLevelExtraData()
        {
            ByType = (int)InvestByType.USD;
        }
        public int PaskageId { get; set; }
        public decimal AmountUSD { get; set; }
        public int ByType { get; set; }
    }
}
