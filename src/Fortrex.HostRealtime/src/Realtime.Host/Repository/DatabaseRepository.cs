using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRCore.Web.Persistence;

namespace SignalRCore.Web.Repository
{
    public class DatabaseRepository : IInventoryRepository
    {
        //private Func<InventoryContext> _contextFactory;

     
        //public DatabaseRepository(Func<InventoryContext> context)
        //{
        //    _contextFactory = context;
        //}

        public string pairname { get; set; }
    }
}
