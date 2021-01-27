using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public enum MarketingEmailType
    {
        SendAll = 1,
        Send_To_User_Received = 2,
        SendTo_ALL_Users_In_List_Table_Temp = 3,
        Send_To_Lock_Account = 4
    }

    public enum BranchStatus
    {
        Avalible = 1,
        Processing = 2,
        Completed = 3
    }
}
