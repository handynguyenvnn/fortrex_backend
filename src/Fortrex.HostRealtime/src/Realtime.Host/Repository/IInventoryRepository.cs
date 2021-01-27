using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRCore.Web.Repository
{
    public interface IInventoryRepository
    {
        string  pairname { get; set; }
        //IEnumerable<Product> Products { get; }

        //Task RegisterProduct(string product, int quantity);

        //Task SellProduct(string product, int quantity);
    }
}
