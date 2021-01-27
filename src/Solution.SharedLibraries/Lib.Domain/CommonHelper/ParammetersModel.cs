using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Common
{ 
    public class ParammetersOrdersModel
    {
        public ParammetersOrdersModel()
        {
            PageIndex = 0;
            PageSize = Constants.PAGE_SIZE_DEFAULT;
        }
        public int UserId { get; set; }
        public string PairName  { get; set; }
        public string SymbolFrom { get; set; }
        public string SymbolTo { get; set; }
        public string Side { get; set; }
        public int[] StatusOrder { get; set; }
        public int PageIndex { get; set; }
        public int  PageSize { get; set; }
    }

    public class ParammetersWithdrawModel
    {
        public ParammetersWithdrawModel()
        {
            PageIndex = 0;
            PageSize = Constants.PAGE_SIZE_DEFAULT;
        }
        public int UserId { get; set; }
        public int[] Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
