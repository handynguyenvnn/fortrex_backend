using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;

namespace Lib.Domain.ModelApi
{
    [DataContract()]
    public class InvestmentPackageResponse
    {
        public InvestmentPackageResponse()
        {
            IsActive = false;
        }

        [DataMember(Name = "packageName", Order = 1)]
        public string PackageName { get; set; }
        [DataMember(Name = "packageAmount", Order = 2)]
        public string PackageAmount { get; set; }
        [DataMember(Name = "isActive", Order = 3)]
        public bool IsActive { get; set; }
        [DataMember(Name = "packageActivated", Order = 3)]
        public string PackageActivated { get; set; }
        [DataMember(Name = "title", Order = 4)]
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string DailyProfit { get; set; }
        public string DetailBonus { get; set; }
        [DataMember(Name = "Descriptions", Order = 5)]
        public string Descriptions { get; set; }
        [DataMember(Name = "linkIcon", Order = 0)]
        public string linkIcon { get; set; }
    }
    
}