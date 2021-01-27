using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class ContractData
    {
        public string to { get; set; }
        public string from { get; set; }
        public decimal amount { get; set; }
    }
    public class AutoReceired
    {
        public string hash { get; set; }
        public int block { get; set; }
        public double timestamp { get; set; }
        public bool confirmed { get; set; }
        public string ownerAddress { get; set; }
        public ContractData contractData { get; set; }
        public string contractType { get; set; }
    }

    public class AutoResponse
    {
        public int total { get; set; }
        public List<AutoReceired> data { get; set; }
    }

    public class AutoReceiredHash
    {
        public string Hash { get; set; }       
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string CreateOn { get; set; }
        public string To { get; set; }
        public string From { get; set; }
    }
}
