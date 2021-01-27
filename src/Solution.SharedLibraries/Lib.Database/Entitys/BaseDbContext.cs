
using LibDatabaseEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SourceCoin.Entitys
{
    public abstract class BaseDbContext
    {
        public BaseDbContext()
        {
            this._db = db;
        }
        protected CoreExchangeDB _db;
       
        
        protected CoreExchangeDB mdb;
       
        protected internal CoreExchangeDB db
        {
            get
            {
                if (mdb == null)
                {
                    mdb = new CoreExchangeDB();
                }

                return mdb;
            }
        }
       
    }

}
