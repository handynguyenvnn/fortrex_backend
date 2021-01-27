using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Domain.Simples
{
    public enum EnumTradeStatusConfirms
    {

        Trading = 1, // đã đặt lệnh
        Confirming = 2, // đang khớp lệnh < 100%
        Confirmed = 3, // đã khớp lệnh hoàn toàn
        Cancel = 0 // hủy lệnh
    }

    
}
