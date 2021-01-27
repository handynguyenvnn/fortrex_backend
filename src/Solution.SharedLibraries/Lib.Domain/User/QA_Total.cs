using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.User
{
    public class QA_Total
    {
        public double TotalDeposit { get; set; }
        public double TotalWithdraw { get; set; }
        public double TotalPending { get; set; }
        public double TotalSend { get; set; }
        public double Again
        {
            get { return TotalDeposit - TotalWithdraw - TotalSend; }
        }
    }
}
